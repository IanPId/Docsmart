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

namespace Delete_M_Files_USers
{
    public partial class Form1 : Form
    {
        private Vault vault = null;
        private MFilesServerApplication app;
        private MFServerConnection conn;

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
        private void EnableButtons(bool enabled)
        {
            btn_DeleteUser.Enabled = enabled;
            btn_disableUser.Enabled = enabled;
        }

        private void EnableTopButtons(bool enabled)
        {
            btn_ListUsers.Enabled = enabled;
            btn_getLogin.Enabled = enabled;
        }

        private void EnableLoginButtons(bool enabled)
        {
            btn_DeleteLogin.Enabled = enabled;
            btn_DisbaleLogin.Enabled = enabled;
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

            return true;
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EnableButtons(false);
            EnableTopButtons(false);
            EnableLoginButtons(false);
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

            //// Populate the combo box so that the user can choose which vault to use.
            //VaultsOnServer vaults = app.GetOnlineVaults();
            //foreach (VaultOnServer v in vaults)
            //{
            //    vaultComboBox.Items.Add(new VaultComboBoxItem(v.Name, v.GUID));
            //}
            //vaultComboBox.Enabled = true;

            //// Must re-start the tool if need to connect to another server.
            //hostTextBox.Enabled = false;
            //connectButton.Enabled = false;
            //EnableTopButtons(true);
            populateVaults();
        }

        private void populateVaults()
        {
            // Populate the combo box so that the user can choose which vault to use.
            VaultsOnServer vaults = app.GetOnlineVaults();
            foreach (VaultOnServer v in vaults)
            {
                vaultComboBox.Items.Add(new VaultComboBoxItem(v.Name, v.GUID));
            }
            vaultComboBox.Enabled = true;

            // Must re-start the tool if need to connect to another server.
            hostTextBox.Enabled = false;
            connectButton.Enabled = false;
            EnableTopButtons(true);
        }

        private void btn_ListUsers_Click(object sender, EventArgs e)
        {
            txt_VaultUsers.Clear();
            if (!ConnectToSelectedVault()) return;

            var vaultUsers = vault.UserOperations.GetUserAccounts();
            foreach (UserAccount userAcc in vaultUsers)
            {
                var LoginAcc = app.LoginAccountOperations.GetLoginAccount(userAcc.LoginName);
                txt_VaultUsers.AppendText("user: " + userAcc.LoginName);
                if (userAcc.Enabled)
                {
                    txt_VaultUsers.AppendText(" Enabled ");
                }
                else
                {
                    txt_VaultUsers.AppendText(" Disabled ");
                }
                txt_VaultUsers.AppendText(LoginAcc.LicenseType.ToString());
                txt_VaultUsers.AppendText(Environment.NewLine);
            }
            EnableButtons(true);
        }


        private void btn_DeleteUser_Click(object sender, EventArgs e)
        {

            var vaultUsers = vault.UserOperations.GetUserAccounts();
            foreach (UserAccount userAcc in vaultUsers)
            {
                if (!userAcc.Enabled)
                {
                    vault.UserOperations.RemoveUserAccount(userAcc.ID);
                }
                EnableButtons(false);
            }
        }

        private void btn_disableUser_Click(object sender, EventArgs e)
        {
            var vaultUsers = vault.UserOperations.GetUserAccounts();
            foreach (UserAccount userAcc in vaultUsers)
            {
                var LoginAcc = app.LoginAccountOperations.GetLoginAccount(userAcc.LoginName);
                if (LoginAcc.LicenseType == MFLicenseType.MFLicenseTypeNone)
                {
                    userAcc.Enabled = false;
                    vault.UserOperations.ModifyUserAccount(userAcc);
                }
                EnableButtons(false);
            }
        }

        private void btn_getLogin_Click(object sender, EventArgs e)
        {
            txt_VaultUsers.Clear();
            var loginAccounts = app.LoginAccountOperations.GetLoginAccounts();
            foreach (LoginAccount userAcc in loginAccounts)
            {
                txt_VaultUsers.AppendText("Login: " + userAcc.AccountName);
                if (userAcc.Enabled)
                {
                    txt_VaultUsers.AppendText(" Enabled ");
                }
                else
                {
                    txt_VaultUsers.AppendText(" Disabled ");
                }
                txt_VaultUsers.AppendText(userAcc.LicenseType.ToString());
                txt_VaultUsers.AppendText(Environment.NewLine);
            }
            EnableLoginButtons(true);
        }

        private void btn_DisbaleLogin_Click(object sender, EventArgs e)
        {
            var loginAccounts = app.LoginAccountOperations.GetLoginAccounts();
            foreach (LoginAccount userAcc in loginAccounts)
            {
                if (userAcc.LicenseType == MFLicenseType.MFLicenseTypeNone)
                {
                    userAcc.Enabled = false;
                    app.LoginAccountOperations.ModifyLoginAccount(userAcc);
                }
            }
            EnableLoginButtons(false);
        }

        private void btn_DeleteLogin_Click(object sender, EventArgs e)
        {
            var loginAccounts = app.LoginAccountOperations.GetLoginAccounts();
            foreach (LoginAccount userAcc in loginAccounts)
            {
                if (!userAcc.Enabled)
                {
                    app.LoginAccountOperations.RemoveLoginAccount(userAcc.AccountName);
                }
            }
            EnableLoginButtons(false);
        }

        private void btn_ResetForm_Click(object sender, EventArgs e)
        {
            txt_VaultUsers.Clear();
            this.Controls.Clear();
            this.InitializeComponent();
            app.Disconnect();
            EnableLoginButtons(false);
            EnableTopButtons(false);
            EnableButtons(false);
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            txt_VaultUsers.Clear();
            vaultComboBox.Items.Clear();
            vaultComboBox.Enabled = true;
            populateVaults();
            EnableLoginButtons(false);
            EnableButtons(false);
        }
    }

}
