using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MFilesAPI;
using MFiles.VAF.Common;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Sdk;
using System.Diagnostics;


namespace FerretMigration
{
    public partial class Form1 : Form
    {
        #region Class members
        // M-Files API objects used for the connection.
        private Vault vault = null;
        private MFilesServerApplication app;
        private MFServerConnection conn;
        private int docCount;
        private int recLimit = 100;
        private int recLoop;

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

        private class SqlDatabaseComboBoxItem
        {
            public string DatabaseName;
            public string DatabaseGuid;

            public SqlDatabaseComboBoxItem(string name, string guid)
            {
                DatabaseName = name;
                DatabaseGuid = guid;
            }
            public override string ToString()
            {
                return DatabaseName;
            }
        }


        #endregion

        public Form1()
        {
            InitializeComponent();
            docCount = 0;
            progressBar1.Visible = false;
            txtStartingRecord.Text = "0";
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

        private void sqlConnectButton_Click(object sender, EventArgs e)
        {
            var server = new Microsoft.SqlServer.Management.Smo.Server();
            //(sqlHostTextBox.Text);
            foreach (Database db in server.Databases)
            {
                sqlDatabaseComboBox.Items.Add(new SqlDatabaseComboBoxItem(db.Name, db.Name));
            }
            // Must re-start the tool if need to connect to another server.
            sqlHostTextBox.Enabled = false;
            sqlConnectButton.Enabled = false;
        }

        private void startMigrationButton_Click(object sender, EventArgs e)
        {
            docCount = 0;
            WriteLog("Inforamtion:", string.Format("Ferret Migration started at {0}", DateTime.Now.ToString()));

            int totalRecords = CountRecords(((SqlDatabaseComboBoxItem)sqlDatabaseComboBox.SelectedItem).DatabaseName, hostTextBox.Text);
            progressBar1.Maximum = totalRecords;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            progressBar1.PerformStep();
            startMigrationButton.Enabled = false;
            if (!singleRecordCheckbox.Checked)
            {
                int i;
                for (i = Convert.ToInt32(txtStartingRecord.Text); i < totalRecords; i += recLimit)
                {
                    if (recLoop == 0)
                    {
                        if (i == 0)
                        {
                            recLoop = recLimit;
                        }
                        else
                        {
                            recLoop = i + recLimit;
                        }
                    }
                    else
                    {
                        recLoop = recLoop + recLimit;
                    }
                    GetFerretData(((SqlDatabaseComboBoxItem)sqlDatabaseComboBox.SelectedItem).DatabaseName,
                    hostTextBox.Text, i, recLoop);
                }
            }
            else
            {
                GetFerretData(((SqlDatabaseComboBoxItem)sqlDatabaseComboBox.SelectedItem).DatabaseName,
                   hostTextBox.Text, Convert.ToInt32(singleRecordTextBox.Text), Convert.ToInt32(singleRecordTextBox.Text));
            }
            progressBar1.Visible = false;

            WriteLog("Inforamtion:", string.Format("Ferret Migration finsihed at {0}", DateTime.Now.ToString()));

            MessageBox.Show(string.Format("Migration Complete: {0} documents migrated", docCount));
            startMigrationButton.Enabled = true;
        }

        public int CountRecords(string DatabaseName, string serverName)
        {
            int RecCount = 0;
            string sqlConnString = string.Format("Data Source ={0}; Initial Catalog = {1}; Integrated Security = SSPI;"
                                  , serverName, DatabaseName);

            using (SqlConnection sqlconnection = new SqlConnection(sqlConnString))
            {
                SqlCommand cmd = new SqlCommand("select Max(DocID) AS [Count] from tblDocument " ,
                                                sqlconnection
                                               );
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        RecCount = reader.GetInt32(0);
                    }
                }
                finally
                {
                    reader.Close();
                    sqlconnection.Close();
                }

            }
            return RecCount;
        }

        public void GetFerretData(string DatabaseName, string serverName, int startRecord, int endRecord)
        {
            //progressBar1.Maximum = CountRecords(DatabaseName, serverName);
            //progressBar1.Step = 1;
            //progressBar1.Value = 0;
            //progressBar1.Visible = true;
            //progressBar1.PerformLayout();

            string sqlConnString = string.Format("Data Source ={0}; Initial Catalog = {1}; Integrated Security = SSPI;"
                                   , serverName, DatabaseName);

            using (SqlConnection sqlconnection = new SqlConnection(sqlConnString))
            {
                SqlCommand cmd = new SqlCommand(string.Format("Select d.DocID, Replace(d.docbarcode,',','') AS [DocumentName], " +
                    "cast(d.DocDate as date) AS [DocumentDate]," +
                    "isnull(d.DocField1, 'Unknown Ferret Doc') AS [DocField1]," +
                    "a.AccountNumber, a.Accountname AS [ClientName]," +
                    "Replace(d.docfield3,',','') AS [Description]," +
                    "d.DocGUID " +
                    "AS [RelativePath], " +
                    "tdd.Extension, " +
                    "d.DocDirPath + '\'  + dbo.GetFolderForAccount(COALESCE (LinkDoc.DocAccountID,d.DocAccountID)) " +
                     "+ '\' + COALESCE (LinkDoc.DocBarCode, d.DocBarCode) + '.' " +
                     "+ COALESCE(LinkDoc.DocFileExt, d.DocFileExt) " +
                     " AS FilePath " +
                    "FROM tblDocument d " +
                    "join t_DocData tdd on d.DocGUID = tdd.ID " +
                    "join tblAccount a on a.AccountID = d.DocAccountID " +
                    "join tblDocType dt on dt.DocTypeID = d.DocDocTypeID " +
                    "join tblType t on t.TypeID = a.TypeID " +
                    "Left Outer Join tblDocument as LinkDoc on d.DocLinkDocID = LinkDoc.DocID " +                   
                    "Where d.DocID between {0} and {1} " +              
                    "Order by d.DocID", startRecord, endRecord),
                    sqlconnection);                
                cmd.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    if (!ObjectExists((int)dr["DocID"]))
                    {
                        ExportFileFromSqlDatabase(Guid.Parse(dr["RelativePath"].ToString()),
                            DatabaseName, serverName
                            );
                        LoadToMfiles(dr["DocumentName"].ToString(), dr["DocumentDate"].ToString(),
                            dr["DocField1"].ToString(), dr["AccountNumber"].ToString(), dr["ClientName"].ToString(),
                            dr["Description"].ToString(), dr["Extension"].ToString(), (int)dr["DocID"], dr["Filepath"].ToString()
                            ,Int32.Parse(dr["desc"].ToString())
                            );
                        progressBar1.PerformStep();
                        docCount = docCount + 1;
                    }


                }
                sqlconnection.Close();
            }
        }

        public static void ExportFileFromSqlDatabase(Guid ID, string DatabaseName, string serverName)
        {
            string sqlConnString = string.Format("Data Source ={0}; Initial Catalog = {1}; Integrated Security = SSPI;"
                                                , serverName, DatabaseName);

            using (SqlConnection sqlconnection = new SqlConnection(sqlConnString))
            {
                sqlconnection.Open();

                string selectQuery = string.Format(@"Select [DocContent] From [t_DocData] Where ID='{0}'"
                                             , ID);

                //  string selectQuery = string.Format(@"Select * from Orgs");

                // Read File content from Sql Table 
                SqlCommand selectCommand = new SqlCommand(selectQuery, sqlconnection);
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    byte[] fileData = (byte[])reader[0];
                    // Write/Export File content into new text file
                    File.WriteAllBytes(@"C:\temp\New_Sample.txt", fileData);
                }
            }

        }
        
        private void LoadToMfiles(string nameOrTitle, string documentDate, string ferretDocumentClass, string accountNumber, string clientName, string description, string extension, int docID, string filepath)
        {
            #region 
            // Which vault are we connecting to?
            var vaultGuid = ((VaultComboBoxItem)vaultComboBox.SelectedItem).VaultGUID;//"{C840BE1A-5B47-4AC0-8EF7-835C166C8E24}";

            // Connect to the vault using a client connection.
            // ref: http://developer.m-files.com/APIs/COM-API/#api-modes-client-vs-server
            // Note: this code will just return the first connection to the vault.
            var clientApplication = new MFilesClientApplication();
            var vault =
                clientApplication
                        .GetVaultConnectionsWithGUID(vaultGuid)
                        .Cast<VaultConnection>()
                        .FirstOrDefault()?
                        .BindToVault(IntPtr.Zero, true, true);
            if (null == vault)
            {
                throw new NotImplementedException("Vault connection not found");
            }

            long length = new System.IO.FileInfo("C:\\temp\\New_Sample.txt").Length;

            // Define the property values for the new object.
            var propertyValues = new MFilesAPI.PropertyValues();

            // Class.
            var classPropertyValue = new MFilesAPI.PropertyValue()
            {
                PropertyDef = (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefClass
            };
            classPropertyValue.Value.SetValue(
                MFDataType.MFDatatypeLookup,  // This must be correct for the property definition.
                                              //(int)MFBuiltInDocumentClass.MFBuiltInDocumentClassOtherDocument
                (int)vault.ClassOperations.GetObjectClassIDByAlias("Class.FerretArchiveDocument")
                );
            propertyValues.Add(-1, classPropertyValue);

            // Name or title.
            var nameOrTitlePropertyValue = new MFilesAPI.PropertyValue()
            {
                PropertyDef = (int)MFBuiltInPropertyDef.MFBuiltInPropertyDefNameOrTitle
            };
            nameOrTitlePropertyValue.Value.SetValue(
                MFDataType.MFDatatypeText,  // This must be correct for the property definition.
                nameOrTitle
            );
            propertyValues.Add(-1, nameOrTitlePropertyValue);

            //Douument Date
            var docDatePropertyValue = new MFilesAPI.PropertyValue()
            {
                PropertyDef = (int)vault.PropertyDefOperations.GetPropertyDefIDByAlias("Prop.DocumentDate")
            };
            docDatePropertyValue.Value.SetValue(
                MFDataType.MFDatatypeDate,
                documentDate
            );
            propertyValues.Add(-1, docDatePropertyValue);

            // Ferret Document Class
            var ferretDocumentClassPropertyValue = new MFilesAPI.PropertyValue()
            {
                PropertyDef = (int)vault.PropertyDefOperations.GetPropertyDefIDByAlias("Prop.FerretDocumentClass")
            };
            ferretDocumentClassPropertyValue.Value.SetValue(
                MFDataType.MFDatatypeText,
                ferretDocumentClass
            );
            propertyValues.Add(-1, ferretDocumentClassPropertyValue);

            // Account Number
            var accountNumberPropertyValue = new MFilesAPI.PropertyValue()
            {
                PropertyDef = (int)vault.PropertyDefOperations.GetPropertyDefIDByAlias("Prop.AccountNumber")
            };
            accountNumberPropertyValue.Value.SetValue(
                MFDataType.MFDatatypeText,
                accountNumber
            );
            propertyValues.Add(-1, accountNumberPropertyValue);

            // Client Name
            var clientNamePropertyValue = new MFilesAPI.PropertyValue()
            {
                PropertyDef = (int)vault.PropertyDefOperations.GetPropertyDefIDByAlias("Prop.ClientName")
            };
            clientNamePropertyValue.Value.SetValue(
                MFDataType.MFDatatypeText,
                clientName
            );
            propertyValues.Add(-1, clientNamePropertyValue);

            // Description
            var descriptionPropertyValue = new MFilesAPI.PropertyValue()
            {
                PropertyDef = (int)vault.PropertyDefOperations.GetPropertyDefIDByAlias("Prop.Description")
            };
            descriptionPropertyValue.Value.SetValue(
                MFDataType.MFDatatypeText,
                description
            );
            propertyValues.Add(-1, descriptionPropertyValue);

            //Ferret Doc Id
            var docIdPropertyValue = new MFilesAPI.PropertyValue()
            {
                PropertyDef = (int)vault.PropertyDefOperations.GetPropertyDefIDByAlias("Prop.FerretDocId")
            };
            docIdPropertyValue.Value.SetValue(
                MFDataType.MFDatatypeInteger,
                docID
            );
            propertyValues.Add(-1, docIdPropertyValue);

            // Define the source files to add.
            var sourceFiles = new MFilesAPI.SourceObjectFiles();

            // Add one file.
            var myFile = new MFilesAPI.SourceObjectFile();
            myFile.SourceFilePath = @"C:\temp\\New_Sample.txt";
            myFile.Title = "My test document"; // For single-file-documents this is ignored.
            myFile.Extension = extension.Replace(".", "");
            sourceFiles.Add(-1, myFile);

            // What object type is being created?
            var objectTypeID = (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument;

            // A "single file document" must be both a document and contain exactly one file.
            var isSingleFileDocument =
                objectTypeID == (int)MFBuiltInObjectType.MFBuiltInObjectTypeDocument && sourceFiles.Count == 1;
            #endregion
            // Create the object and check it in.
            try
            {
                if (length != 0)
                {
                    var objectVersion = vault.ObjectOperations.CreateNewObjectEx(
                        objectTypeID,
                        propertyValues,
                        sourceFiles,
                        SFD: isSingleFileDocument,
                        CheckIn: true);
                    string msg = string.Format("Record {0}", docID);
                    WriteLog("Success:", msg);
                }
                else
                {
                    // Define the source files to add.
                    //var sourceFiles1 = new MFilesAPI.SourceObjectFiles();

                    //// Add one file.
                    //var myFile1 = new MFilesAPI.SourceObjectFile();
                    //myFile1.SourceFilePath = filepath;
                    //myFile1.Title = "My test document"; // For single-file-documents this is ignored.
                    //myFile1.Extension = extension.Replace(".", "");
                    //sourceFiles1.Add(-1, myFile1);

                    //var objectVersion = vault.ObjectOperations.CreateNewObjectEx(
                    //    objectTypeID,
                    //    propertyValues,
                    //    sourceFiles1,
                    //    SFD: isSingleFileDocument,
                    //    CheckIn: true);
                    //string msg = string.Format("Record {0}", docID);
                    //WriteLog("Success:", msg);
                    string ErrorMsg = string.Format("Record {0}, Document Name {1} dated {2} for account {3}. Msg is {4} ", docID, nameOrTitle, documentDate, accountNumber, "File size 0"); // e.Message);
                    WriteLog("Error:", ErrorMsg);
                   // string loadLog = string.Format("{0},(1},{2},{3},{4},{5},{6}", nameOrTitle, documentDate, ferretDocumentClass, accountNumber, clientName, description, filepath);
                  //  WriteErrorLog(loadLog);
                }
            }
            catch (System.IO.IOException e)
            {
                //try
                //{
                //    // Define the source files to add.
                //    var sourceFiles1 = new MFilesAPI.SourceObjectFiles();

                //    // Add one file.
                //    var myFile1 = new MFilesAPI.SourceObjectFile();
                //    myFile1.SourceFilePath = filepath;
                //    myFile1.Title = "My test document"; // For single-file-documents this is ignored.
                //    myFile1.Extension = extension.Replace(".", "");
                //    sourceFiles1.Add(-1, myFile1);

                //    var objectVersion = vault.ObjectOperations.CreateNewObjectEx(
                //        objectTypeID,
                //        propertyValues,
                //        sourceFiles1,
                //        SFD: isSingleFileDocument,
                //        CheckIn: true);
                //    string msg = string.Format("Record {0}", docID);
                //    WriteLog("Success:", msg);
                //}
                //catch
                {
                    string ErrorMsg = string.Format("Record {0}, Document Name {1} dated {2} for account {3}. Msg is {4} ", docID, nameOrTitle, documentDate, accountNumber, e.Message);
                    WriteLog("Error:", ErrorMsg);
                    string loadLog = string.Format("{0},(1},{2},{3},{4},{5},{6}", nameOrTitle, documentDate, ferretDocumentClass, accountNumber, clientName, description, filepath);
                    WriteErrorLog(loadLog);
                }
            }
        }

        private void WriteLog(string logType, string logMessage)
        {
            using (StreamWriter writer = new StreamWriter("C:\\temp\\Migrationlog.txt", true))
            {
                writer.WriteLine(string.Format("{0} {1}", logType, logMessage));
            }
        }
        private void WriteErrorLog(string logMessage)
        {
            using (StreamWriter writer = new StreamWriter("C:\\temp\\Error.txt", true))
            {
                writer.WriteLine(string.Format("{0}", logMessage));
            }
        }
        public bool ObjectExists(int id)
        {
            var vaultGuid = ((VaultComboBoxItem)vaultComboBox.SelectedItem).VaultGUID;//"{C840BE1A-5B47-4AC0-8EF7-835C166C8E24}";

            // Connect to the vault using a client connection.
            // ref: http://developer.m-files.com/APIs/COM-API/#api-modes-client-vs-server
            // Note: this code will just return the first connection to the vault.
            var clientApplication = new MFilesClientApplication();
            var vault =
                clientApplication
                        .GetVaultConnectionsWithGUID(vaultGuid)
                        .Cast<VaultConnection>()
                        .FirstOrDefault()?
                        .BindToVault(IntPtr.Zero, true, true);
            if (null == vault)
            {
                throw new NotImplementedException("Vault connection not found");
            }
            var objectSearch = new MFSearchBuilder(vault);
            objectSearch.Deleted(false);
            objectSearch.ObjType(MFBuiltInObjectType.MFBuiltInObjectTypeDocument);
            objectSearch.Class((int)vault.ClassOperations.GetObjectClassIDByAlias("Class.FerretArchiveDocument"));
            objectSearch.Conditions.AddPropertyCondition(
                (int)vault.PropertyDefOperations.GetPropertyDefIDByAlias("Prop.FerretDocId"),
                MFConditionType.MFConditionTypeEqual,
                MFDataType.MFDatatypeInteger,
                id);
            var objectResults = objectSearch.Find();
            if (objectResults.Count >= 1)
            {
                return (true);
            }
            return (false);
        }
    }
}
