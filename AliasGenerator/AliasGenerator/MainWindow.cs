using System;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using MFilesAPI;

// NOTE: In case of problems, make sure that you have set the correct M-Files API DLL version in the 
// project references!

// This is a quick-and-dirty tool for generating and clearing aliases in a vault. It has NOT been fully tested,
// and is NOT officially supported in any way. Use at your own risk, and feel free to improve the tool anyway
// you need.

// Please read the associated readme.txt for more details.

// Created by: Petri Niemi / M-Files --- Any improvement ideas welcome, but actual improvements not guaranteed.

namespace AliasGenerator
{
    public partial class MainWindow : Form
    {
        #region Class members
        // M-Files API objects used for the connection.
        private Vault vault = null;
        private MFilesServerApplication app;
        private MFServerConnection conn;

        // The prefixes set by the user.
        private string otPrefix;        // Object type
        private string clPrefix;        // Class
        private string vlPrefix;        // Value list
        private string pdPrefix;        // Property definition
        private string wfPrefix;        // Workflow
        private string wfsPrefix;       // Workflow state

        // Lists of generated aliases (used when the "Alias.cs" and "Config.cs" are generated once all the aliases are in place.
        List<Alias> otAliases = new List<Alias>();      // Object types
        List<Alias> clAliases = new List<Alias>();      // Classes
        List<Alias> vlAliases = new List<Alias>();      // Value lists
        List<Alias> pdAliases = new List<Alias>();      // Property definitions
        List<Alias> wfAliases = new List<Alias>();      // Workflows
        List<Alias> wfsAliases = new List<Alias>();     // Workflow states
    
        // A private class for the items put into the Vault selection ComboBox.
        private class VaultComboBoxItem
        {
            public string VaultName;
            public string VaultGUID;

            public VaultComboBoxItem(string name, string guid)
            {
                VaultName = name;
                VaultGUID = guid;

            }
            public override string ToString()
            {
                // Generates the text shown in the combo box
                return VaultName + " " + VaultGUID;
            }
        }
        
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }


        #region Click handler methods to connect and start creating aliases

        private void connectButton_Click(object sender, EventArgs e)
        {
            // Using the Server mode to connect to the M-Files Server.
            app = new MFilesServerApplication();

            string host = hostTextBox.Text;

            // Using the default values for connecting to server (HTTP, 2266, current Windows user, etc.), 
            // for details and options see:
            // https://www.m-files.com/api/documentation/latest/index.html#MFilesAPI~MFilesServerApplication.html
            conn = app.Connect(NetworkAddress: host);

            // Populate the combo box so that the user can choose which vault to use.
            VaultsOnServer vaults = app.GetOnlineVaults();
            foreach (VaultOnServer v in vaults)
            {
                vaultComboBox.Items.Add(new VaultComboBoxItem(v.Name, v.GUID));
            }
            vaultComboBox.Enabled = true;
            EnableButtons(true);

            // Must re-start the tool if need to connect to another server.
            hostTextBox.Enabled = false;
            connectButton.Enabled = false;
        }

        // "Start" button clicked
        private void startButton_Click(object sender, EventArgs e)
        {
            // Connect to the vault and continue if successful.
            if (!ConnectToSelectedVault()) return;

            // Read the prefixes set by the user.
            ReadPrefixes();

            // Start creating aliases
            if (otCheckBox.Checked) CreateObjectTypeAliases();
            if (clCheckBox.Checked) CreateClassAliases();
            if (pdCheckBox.Checked) CreatePropertyDefAliases();
            if (vlCheckBox.Checked) CreateValueListAliases();
            CreateWorkflowAndStateAliases();   // Check boxes inspected within the method

            // Also create the "Aliases.cs" and "Config.cs" files if needed.
            if (aliasesClassCheckBox.Checked)
            {
                string path = aliasesClassPathTextBox.Text;
                string className = aliasesClassNameTextBox.Text;

                // TODO: Hard-coding the namespace "VaultAliases" for now.
                AliasClassWriter aliasWriter = new AliasClassWriter(path, "VaultAliases", className);
                aliasWriter.Initialize();
                aliasWriter.WriteAliases(otAliases, "Object type aliases");
                aliasWriter.WriteAliases(clAliases, "Class aliases");
                aliasWriter.WriteAliases(vlAliases, "Value list aliases");
                aliasWriter.WriteAliases(pdAliases, "Property definition aliases");
                aliasWriter.WriteAliases(wfAliases, "Workflow aliases");
                aliasWriter.WriteAliases(wfsAliases, "Workflow state aliases");
                aliasWriter.Close();
            }

            if (configClassCheckBox.Checked)
            {
                string path = configClassPathTextBox.Text;
                string className = configClassNameTextBox.Text;

                // TODO: Hard-coding the namespace "VaultAliases" for now (note the second namespace needs to match the namespace
                // used for "Aliases.cs" above).
                ConfigClassWriter configWriter = new ConfigClassWriter(path, "VaultAliases", "VaultAliases", className);
                configWriter.Initialize();
                configWriter.WriteAliases(otAliases, "Object types");
                configWriter.WriteAliases(clAliases, "Classes");
                configWriter.WriteAliases(vlAliases, "Value lists");
                configWriter.WriteAliases(pdAliases, "Property definitions");
                configWriter.WriteAliases(wfAliases, "Workflows");
                configWriter.WriteAliases(wfsAliases, "Workflow states");
                configWriter.Close();
            }

            MessageBox.Show("Done! Please check with M-Files Admin that there are no duplicate aliases within a single metadata element type.");
            EnableButtons(true);
        } 
        #endregion


        #region Click handler methods to start clearing aliases

		private void clearSelectedButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to clear the specified aliases in the vault?", "Clearing specified aliases", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // Connect to the vault and continue if successful.
                if (!ConnectToSelectedVault()) return;

                // Read the prefixes set by the user.
                ReadPrefixes();

                if (otCheckBox.Checked) ClearSelectedObjectTypeAliases();
                if (clCheckBox.Checked) ClearSelectedClassAliases();
                if (pdCheckBox.Checked) ClearSelectedPropertyDefAliases();
                if (vlCheckBox.Checked) ClearSelectedValueListAliases();
                ClearSelectedWorkflowAndStateAliases();   // Check boxes inspected within the method

                MessageBox.Show("Done! Please check with M-Files Admin that everything worked as expected.");
                EnableButtons(true);
            }
        }

        private void clearAllButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you REALLY want to clear all aliases in the vault?", "Clearing all aliases", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                // Connect to the vault and continue if successful.
                if (!ConnectToSelectedVault()) return;

                // Read the prefixes set by the user.
                ReadPrefixes();

                if (otCheckBox.Checked) ClearAllObjectTypeAliases();
                if (clCheckBox.Checked) ClearAllClassAliases();
                if (pdCheckBox.Checked) ClearAllPropertyDefAliases();
                if (vlCheckBox.Checked) ClearAllValueListAliases();
                ClearAllWorkflowAndStateAliases();   // Check boxes inspected within the method

                MessageBox.Show("Done! Please check with M-Files Admin that everything worked as expected.");
                EnableButtons(true);
            }
        }
 
	    #endregion

        
        #region Methods to create the specified aliases

        private void CreateObjectTypeAliases()
        {
            ObjTypesAdmin objectTypes = vault.ObjectTypeOperations.GetObjectTypesAdmin();

            foreach (ObjTypeAdmin ot in objectTypes)
            {
                // Skip built-in object types like Document, Assignment and Report. Modify this if you
                // need to include one or more of these.
                if (ot.ObjectType.ID < 100) continue;

                string otName = ot.ObjectType.NameSingular.Replace(" ", "");   //String.Empty); //Removed _
                string aliases = ot.SemanticAliases.Value;

                Alias newAlias = new Alias(Alias.ELEMENT_TYPE_OT, otName, otPrefix);

                // Add the new alias if not already there.
                string updatedAliases = AddAlias(aliases, newAlias.ElementAlias);
                if (aliases != updatedAliases)
                {
                    ot.SemanticAliases.Value = updatedAliases;
                    vault.ObjectTypeOperations.UpdateObjectTypeAdmin(ot);
                }

                // We will need the alias object later when generating the "Aliases.cs" and "Config.cs" files.
                otAliases.Add(newAlias);
            }
        }

        private void CreateClassAliases()
        {
            ObjectClasses classes = vault.ClassOperations.GetAllObjectClasses();
            foreach (ObjectClass cl in classes)
            {
                // Skip Assignment and Report classes (comment this line out if you want to include them).
                if (cl.ID < 0) continue;

                // The corresponding ObjectClassAdmin
                ObjectClassAdmin cla = vault.ClassOperations.GetObjectClassAdmin(cl.ID);

                string clName = cla.Name.Replace(" ", "");     //String.Empty);
                string aliases = cla.SemanticAliases.Value;

                Alias newAlias = new Alias(Alias.ELEMENT_TYPE_CL, clName, clPrefix);

                // Add the new alias if not already there.
                string updatedAliases = AddAlias(aliases, newAlias.ElementAlias);
                if (aliases != updatedAliases)
                {
                    // Need to get the AssociatedPropertyDefs from the underlying ObjectClass (except
                    // for a few built-in properties), otherwise UpdateObjectClassAdmin() will fail.
                    CopyPropertiesToObjectClassAdmin(cla, cl);

                    cla.SemanticAliases.Value = updatedAliases;
                    vault.ClassOperations.UpdateObjectClassAdmin(cla);
                }

                // We will need the alias object later when generating the "Aliases.cs" and "Config.cs" files.
                clAliases.Add(newAlias);
            }
        }

        private void CreatePropertyDefAliases()
        {
            PropertyDefsAdmin propDefs = vault.PropertyDefOperations.GetPropertyDefsAdmin();

            foreach (PropertyDefAdmin pda in propDefs)
            {
                // Skip built-in properties. Modify this if you need to include one or more of these.
                if (pda.PropertyDef.ID <= 101) continue;

                string pdName = pda.PropertyDef.Name.Replace(" ", "");   //String.Empty);
                string aliases = pda.SemanticAliases.Value;

                Alias newAlias = new Alias(Alias.ELEMENT_TYPE_PD, pdName, pdPrefix);

                // Add the new alias if not already there.
                string updatedAliases = AddAlias(aliases, newAlias.ElementAlias);

                if (aliases != updatedAliases)
                {
                    pda.SemanticAliases.Value = updatedAliases;
                    vault.PropertyDefOperations.UpdatePropertyDefAdmin(pda);
                }

                // We will need the alias object later when generating the "Aliases.cs" and "Config.cs" files.
                pdAliases.Add(newAlias);
            }
        }

        private void CreateValueListAliases()
        {
            ObjTypesAdmin valueLists = vault.ValueListOperations.GetValueListsAdmin();

            foreach (ObjTypeAdmin vl in valueLists)
            {
                // Only interested in value lists, not object types. Skip built-in value lists as well. 
                // Modify this if you need to set value list style aliases to object types as well, or include
                // one or more of the built-in value lists.
                if (vl.ObjectType.RealObjectType || vl.ObjectType.ID < 100) continue;

                string vlName = vl.ObjectType.NamePlural.Replace(" ", "");   //String.Empty);
                string aliases = vl.SemanticAliases.Value;

                Alias newAlias = new Alias(Alias.ELEMENT_TYPE_VL, vlName, vlPrefix);

                // Add the new alias if not already there.
                string updatedAliases = AddAlias(aliases, newAlias.ElementAlias);
                if (aliases != updatedAliases)
                {
                    vl.SemanticAliases.Value = updatedAliases;
                    vault.ValueListOperations.UpdateValueListAdmin(vl);
                }

                // We will need the alias object later when generating the "Aliases.cs" and "Config.cs" files.
                vlAliases.Add(newAlias);
            }
        }

        private void CreateWorkflowAndStateAliases()
        {
            WorkflowsAdmin wfs = vault.WorkflowOperations.GetWorkflowsAdmin();

            foreach (WorkflowAdmin wf in wfs)
            {
                string wfName = wf.Workflow.Name.Replace(" ", "");   //String.Empty);
                string aliases = wf.SemanticAliases.Value;

                if (wfCheckBox.Checked)
                {
                    Alias newAlias = new Alias(Alias.ELEMENT_TYPE_WF, wfName, wfPrefix);

                    // Add the new alias if not already there.
                    string updatedAliases = AddAlias(aliases, newAlias.ElementAlias);
                    if (aliases != updatedAliases)
                    {
                        wf.SemanticAliases.Value = updatedAliases;
                    }
                    // We will need the alias object later when generating the "Aliases.cs" and "Config.cs" files.
                    wfAliases.Add(newAlias);
                }

                if (wfsCheckBox.Checked)
                {
                    // Also go through each state in the workflow.
                    foreach (StateAdmin state in wf.States)
                    {
                        string stateName = state.Name.Replace(" ", "");   //String.Empty);
                        string stateAliases = state.SemanticAliases.Value;

                        // TODO: Or just stateName and not wfName.stateName?
                        Alias newAlias = new Alias(Alias.ELEMENT_TYPE_WFS, wfName + "." + stateName, wfsPrefix);   

                        string updatedStateAliases = AddAlias(stateAliases, newAlias.ElementAlias); 
                        if (stateAliases != updatedStateAliases)
                        {
                            state.SemanticAliases.Value = updatedStateAliases;
                        }
                        // We will need the alias object later when generating the "Aliases.cs" and "Config.cs" files.
                        wfsAliases.Add(newAlias);
                    }
                }

                // TODO: also set state transition aliases?

                // Update only if something was possibly modified.
                if (wfCheckBox.Checked || wfsCheckBox.Checked)
                {
                    vault.WorkflowOperations.UpdateWorkflowAdmin(wf);
                }
            }
        } 
        #endregion

       
        #region Methods to clear the specified aliases

        // Note: Will go through ALL object types, even built-in ones (modify the code if different behavior
        // is needed).
        private void ClearSelectedObjectTypeAliases()
        {
            ObjTypesAdmin objectTypes = vault.ObjectTypeOperations.GetObjectTypesAdmin();

            foreach (ObjTypeAdmin ot in objectTypes)
            {
                string otName = ot.ObjectType.NameSingular.Replace(" ", "");   //String.Empty);
                string aliases = ot.SemanticAliases.Value;

                // Remove the aliasPrefix if it exists.
                string updatedAliases = RemoveAlias(aliases, otPrefix + otName);
                if (aliases != updatedAliases)
                {
                    ot.SemanticAliases.Value = updatedAliases;
                    vault.ObjectTypeOperations.UpdateObjectTypeAdmin(ot);
                }
            }
        }
        
        // Note: Will go through ALL class, even built-in ones (modify the code if different behavior
        // is needed).
        private void ClearSelectedClassAliases()
        {
            ObjectClasses classes = vault.ClassOperations.GetAllObjectClasses();
            foreach (ObjectClass cl in classes)
            {
                // The corresponding ObjectClassAdmin
                ObjectClassAdmin cla = vault.ClassOperations.GetObjectClassAdmin(cl.ID);

                string clName = cla.Name.Replace(" ", "");     //String.Empty);
                string aliases = cla.SemanticAliases.Value;

                // Remove the aliasPrefix if it exists.
                string updatedAliases = RemoveAlias(aliases, clPrefix + clName);
                if (aliases != updatedAliases)
                {
                    // Need to get the AssociatedPropertyDefs from the underlying ObjectClass (except
                    // for a few built-in properties), otherwise UpdateObjectClassAdmin() will fail.
                    CopyPropertiesToObjectClassAdmin(cla, cl);

                    cla.SemanticAliases.Value = updatedAliases;
                    vault.ClassOperations.UpdateObjectClassAdmin(cla);
                }
            }
        }

        // Note: Will go through ALL property defs, even built-in ones (modify the code if different behavior
        // is needed).
        private void ClearSelectedPropertyDefAliases()
        {
            PropertyDefsAdmin propDefs = vault.PropertyDefOperations.GetPropertyDefsAdmin();

            foreach (PropertyDefAdmin pda in propDefs)
            {
                string pdName = pda.PropertyDef.Name.Replace(" ", "");   //String.Empty);
                string aliases = pda.SemanticAliases.Value;

                // Remove the aliasPrefix if it exists.
                string updatedAliases = RemoveAlias(aliases, pdPrefix + pdName);

                if (aliases != updatedAliases)
                {
                    pda.SemanticAliases.Value = updatedAliases;
                    vault.PropertyDefOperations.UpdatePropertyDefAdmin(pda);
                }
            }
        }

        // Note: Will go through ALL value lists, even built-in ones (modify the code if different behavior
        // is needed).
        private void ClearSelectedValueListAliases()
        {
            ObjTypesAdmin valueLists = vault.ValueListOperations.GetValueListsAdmin();

            foreach (ObjTypeAdmin vl in valueLists)
            {
                // Only interested in value lists, not object types.
                if (vl.ObjectType.RealObjectType) continue;

                string vlName = vl.ObjectType.NamePlural.Replace(" ", "");   //String.Empty);
                string aliases = vl.SemanticAliases.Value;

                // Remove the aliasPrefix if it exists.
                string updatedAliases = RemoveAlias(aliases, vlPrefix + vlName);
                if (aliases != updatedAliases)
                {
                    vl.SemanticAliases.Value = updatedAliases;
                    vault.ValueListOperations.UpdateValueListAdmin(vl);
                }
            }
        }

        // Note: Will go through ALL workflows and their states.
        private void ClearSelectedWorkflowAndStateAliases()
        {
            WorkflowsAdmin wfs = vault.WorkflowOperations.GetWorkflowsAdmin();

            foreach (WorkflowAdmin wf in wfs)
            {
                string wfName = wf.Workflow.Name.Replace(" ", "");   //String.Empty);
                string aliases = wf.SemanticAliases.Value;

                if (wfCheckBox.Checked)
                {
                    // Remove the aliasPrefix if it exists.
                    string updatedAliases = RemoveAlias(aliases, wfPrefix + wfName);
                    if (aliases != updatedAliases)
                    {
                        wf.SemanticAliases.Value = updatedAliases;
                    }
                }

                if (wfsCheckBox.Checked)
                {
                    // Also go through each state in the workflow
                    foreach (StateAdmin state in wf.States)
                    {
                        string stateName = state.Name.Replace(" ", "");   //String.Empty);
                        string stateAliases = state.SemanticAliases.Value;

                        // Remove the aliasPrefix if it exists.
                        string updatedStateAliases = RemoveAlias(stateAliases, wfsPrefix + wfName + "." + stateName); // or: wfsPrefix + stateName ??
                        if (stateAliases != updatedStateAliases)
                        {
                            state.SemanticAliases.Value = updatedStateAliases;
                        }
                    }
                }

                // TODO: also remove state transition aliases?

                // Update only if something was possibly modified
                if (wfCheckBox.Checked || wfsCheckBox.Checked)
                {
                    vault.WorkflowOperations.UpdateWorkflowAdmin(wf);
                }
            }
        } 
        #endregion
        

        #region Methods to clear all aliases

        // Note: Will go through ALL object types, even built-in ones (modify the code if different behavior
        // is needed).
        private void ClearAllObjectTypeAliases()
        {
            ObjTypesAdmin objectTypes = vault.ObjectTypeOperations.GetObjectTypesAdmin();

            foreach (ObjTypeAdmin ot in objectTypes)
            {
                ot.SemanticAliases.Value = "";
                vault.ObjectTypeOperations.UpdateObjectTypeAdmin(ot);
            }
        }

        // Note: Will go through ALL classes, even built-in ones (modify the code if different behavior
        // is needed).
        private void ClearAllClassAliases()
        {
            ObjectClasses classes = vault.ClassOperations.GetAllObjectClasses();
            foreach (ObjectClass cl in classes)
            {
                // The corresponding ObjectClassAdmin
                ObjectClassAdmin cla = vault.ClassOperations.GetObjectClassAdmin(cl.ID);

                // Need to get the AssociatedPropertyDefs from the underlying ObjectClass (except
                // for a few built-in properties), otherwise UpdateObjectClassAdmin() will fail.
                CopyPropertiesToObjectClassAdmin(cla, cl);

                cla.SemanticAliases.Value = "";
                vault.ClassOperations.UpdateObjectClassAdmin(cla);
            }
        }

        // Note: Will go through ALL property defs, even built-in ones (modify the code if different behavior
        // is needed).
        private void ClearAllPropertyDefAliases()
        {
            PropertyDefsAdmin propDefs = vault.PropertyDefOperations.GetPropertyDefsAdmin();

            foreach (PropertyDefAdmin pda in propDefs)
            {
                pda.SemanticAliases.Value = "";
                vault.PropertyDefOperations.UpdatePropertyDefAdmin(pda);
            }
        }

        // Note: Will go through ALL value lists, even built-in ones (modify the code if different behavior
        // is needed).
        private void ClearAllValueListAliases()
        {
            ObjTypesAdmin valueLists = vault.ValueListOperations.GetValueListsAdmin();

            foreach (ObjTypeAdmin vl in valueLists)
            {
                // Only interested in value lists, not object types.
                if (vl.ObjectType.RealObjectType) continue;

                vl.SemanticAliases.Value = "";
                vault.ValueListOperations.UpdateValueListAdmin(vl);
            }
        }

        // Note: Will go through ALL workflows and their states.
        private void ClearAllWorkflowAndStateAliases()
        {
            WorkflowsAdmin wfs = vault.WorkflowOperations.GetWorkflowsAdmin();

            foreach (WorkflowAdmin wf in wfs)
            {
                if (wfCheckBox.Checked)
                {
                    wf.SemanticAliases.Value = "";
                }

                if (wfsCheckBox.Checked)
                {
                    // Also go through each state in the workflow
                    foreach (StateAdmin state in wf.States)
                    {
                        state.SemanticAliases.Value = "";
                    }
                }

                vault.WorkflowOperations.UpdateWorkflowAdmin(wf);
            }
        }
        #endregion


        #region Utility methods for all the above

        // Connects to the vault selected by the user.
        private bool ConnectToSelectedVault()
        {
            if (vaultComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select the target vault.");
                return false;
            }

            string currentlySelectedVaultGUID = ((VaultComboBoxItem)vaultComboBox.SelectedItem).VaultGUID;

            // No need to re-connect if already connected to the selected vault.
            if (vault == null || !vault.GetGUID().Equals(currentlySelectedVaultGUID))
            {
                vault = app.LogInToVault(currentlySelectedVaultGUID);
                Console.WriteLine("Connected to: " + vault.Name);
            }

            vaultComboBox.Enabled = false;  // Must re-start the app if the vault needs to be changed.
            EnableButtons(false);

            return true;
        }

        // Reads the prefixes set by the user.
        private void ReadPrefixes()
        {
            // Modify these if you want to use some hard-coded values instead.
            otPrefix = otPrefixTextBox.Text;
            clPrefix = clPrefixTextBox.Text;
            vlPrefix = vlPrefixTextBox.Text;
            pdPrefix = pdPrefixTextBox.Text;
            wfPrefix = wfPrefixTextBox.Text;
            wfsPrefix = wfsPrefixTextBox.Text;
        }

        // Adds the given aliasPrefix to the list of current aliases, if it does not already exist.
        // The implementation is very inefficient, but that should not matter in this case.
        private string AddAlias(string aliases, string newAlias)
        {
            // If the aliasPrefix is already there, do nothing.
            string[] aliasesArray = aliases.Split(';');
            foreach (string alias in aliasesArray)
            {
                if (alias == newAlias)
                {
                    Console.WriteLine("Skipping \"" + newAlias + "\", already exists.");
                    return aliases;
                }
            }

            // Must use ';' as separator if there already is some content in the aliases.
            if (aliases.Length != 0)
            {
                aliases = aliases + ";";
            }
            Console.WriteLine("Adding \"" + newAlias + "\".");
            return aliases + newAlias;
        }

        // Removes the given aliasPrefix from the list of current aliases, if it exists.
        // The implementation is very inefficient, but that should not matter in this case.
        private string RemoveAlias(string aliases, string aliasToBeRemoved)
        {
            // Split the current aliases into separate values.
            string[] aliasesArray = aliases.Split(';');

            // The new aliases.
            string newAliases = "";

            foreach (string alias in aliasesArray)
            {
                if (alias != aliasToBeRemoved)
                {
                    newAliases += alias + ";";
                }
            }

            if (newAliases.Length > 0 && newAliases.EndsWith(";"))
            {
                // Remove the extra ";" from the end
                newAliases = newAliases.Remove(newAliases.Length - 1);
            }

            return newAliases;
        }

        // Copies properties from the given ObjectClass to ObjectClassAdmin (these are not copied by default,
        // even though the ObjectClassAdmin is based on the ObjectClass).
        private void CopyPropertiesToObjectClassAdmin(ObjectClassAdmin oca, ObjectClass oc)
        {
            oca.AssociatedPropertyDefs = new AssociatedPropertyDefs();

            // Skip certain built-in properties present in the original ObjectClass object, these cause problems with
            // UpdateObjectClassAdmin() later.
            foreach (AssociatedPropertyDef def in oc.AssociatedPropertyDefs)
            {
                if ((def.PropertyDef >= 20 && def.PropertyDef <= 25) ||
                    (def.PropertyDef >= 30 && def.PropertyDef <= 32) ||
                    def.PropertyDef == 89 ||
                    def.PropertyDef == 101)
                {
                    continue;
                }
                else
                {
                    //Console.WriteLine("Adding: " + oc.Name + ", " + def.PropertyDef);
                    oca.AssociatedPropertyDefs.Add(-1, def);
                }
            }
        }

        private void EnableButtons(bool enabled)
        {
            startButton.Enabled = enabled;
            clearSelectedButton.Enabled = enabled;
            clearAllButton.Enabled = enabled;
        }

        #endregion

        private void otPrefixTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
