using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MFilesAPI;

namespace Vault_Updater
{
    public partial class Form1 : Form
    {
        #region Class members
        // M-Files API objects used for the connection.
        private Vault vault = null;
        private MFilesServerApplication app;
        private MFServerConnection conn;


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

        private class PropertyComboBoxItem
        {
            public string PropName;
            public int propID;

            public PropertyComboBoxItem(string name, int id)
            {
                PropName = name;
                propID = id;
            }
            public override string ToString()
            {
                return PropName + " " + propID;
            }


        }

        #endregion

        public Form1()
        {
            InitializeComponent();
        }

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
            //       EnableButtons(true);

            // Must re-start the tool if need to connect to another server.
            hostTextBox.Enabled = false;
            connectButton.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Connect to the vault and continue if successful.
            if (!ConnectToSelectedVault()) return;

            PropertyDefsAdmin propDefs = vault.PropertyDefOperations.GetPropertyDefsAdmin();

            foreach (PropertyDefAdmin pda in propDefs)
            {
                propertyComboBox.Items.Add(new PropertyComboBoxItem(pda.PropertyDef.Name, pda.PropertyDef.ID));
            }


        }

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
          //  EnableButtons(false);

            return true;
        }

        private void btnAddToClass_Click(object sender, EventArgs e)
        {
            // Connect to the vault and continue if successful.
            if (!ConnectToSelectedVault()) return;

            ObjectClasses classes = vault.ClassOperations.GetAllObjectClasses();
            foreach (ObjectClass cl in classes)
            {
                if (cl.ObjectType == 0)
                {
                    ObjectClassAdmin cla = vault.ClassOperations.GetObjectClassAdmin(cl.ID);
                    // Need to get the AssociatedPropertyDefs from the underlying ObjectClass (except
                    // for a few built-in properties), otherwise UpdateObjectClassAdmin() will fail.
                    CopyPropertiesToObjectClassAdmin(cla, cl);
                    AssociatedPropertyDef apd = new AssociatedPropertyDef();
                    apd.PropertyDef = ((PropertyComboBoxItem)propertyComboBox.SelectedItem).propID;
                    cla.AssociatedPropertyDefs.Add(-1, apd);
                    vault.ClassOperations.UpdateObjectClassAdmin(cla);

                    
                }
            }
            MessageBox.Show("Done");

        }
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
    }
}
