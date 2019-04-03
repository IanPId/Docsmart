namespace Delete_M_Files_USers
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.connectButton = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.hostLabel = new System.Windows.Forms.Label();
            this.vaultComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_ListUsers = new System.Windows.Forms.Button();
            this.txt_VaultUsers = new System.Windows.Forms.TextBox();
            this.btn_DeleteUser = new System.Windows.Forms.Button();
            this.btn_disableUser = new System.Windows.Forms.Button();
            this.btn_getLogin = new System.Windows.Forms.Button();
            this.btn_DisbaleLogin = new System.Windows.Forms.Button();
            this.btn_DeleteLogin = new System.Windows.Forms.Button();
            this.btn_ResetForm = new System.Windows.Forms.Button();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(15, 31);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(155, 23);
            this.connectButton.TabIndex = 40;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(188, 35);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(473, 13);
            this.label14.TabIndex = 39;
            this.label14.Text = "(Connection defaults: TCP/IP, port 2266, current Windows user. Modify app soure c" +
    "ode if needed.)";
            // 
            // hostTextBox
            // 
            this.hostTextBox.Location = new System.Drawing.Point(191, 7);
            this.hostTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(268, 20);
            this.hostTextBox.TabIndex = 35;
            this.hostTextBox.Text = "localhost";
            // 
            // hostLabel
            // 
            this.hostLabel.AutoSize = true;
            this.hostLabel.Location = new System.Drawing.Point(12, 9);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Size = new System.Drawing.Size(64, 13);
            this.hostLabel.TabIndex = 38;
            this.hostLabel.Text = "Server host:";
            // 
            // vaultComboBox
            // 
            this.vaultComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vaultComboBox.Enabled = false;
            this.vaultComboBox.FormattingEnabled = true;
            this.vaultComboBox.Location = new System.Drawing.Point(191, 110);
            this.vaultComboBox.Name = "vaultComboBox";
            this.vaultComboBox.Size = new System.Drawing.Size(268, 21);
            this.vaultComboBox.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Target vault:";
            // 
            // btn_ListUsers
            // 
            this.btn_ListUsers.Location = new System.Drawing.Point(15, 146);
            this.btn_ListUsers.Name = "btn_ListUsers";
            this.btn_ListUsers.Size = new System.Drawing.Size(75, 23);
            this.btn_ListUsers.TabIndex = 41;
            this.btn_ListUsers.Text = "Get Users";
            this.btn_ListUsers.UseVisualStyleBackColor = true;
            this.btn_ListUsers.Click += new System.EventHandler(this.btn_ListUsers_Click);
            // 
            // txt_VaultUsers
            // 
            this.txt_VaultUsers.Location = new System.Drawing.Point(15, 176);
            this.txt_VaultUsers.Multiline = true;
            this.txt_VaultUsers.Name = "txt_VaultUsers";
            this.txt_VaultUsers.Size = new System.Drawing.Size(773, 212);
            this.txt_VaultUsers.TabIndex = 42;
            // 
            // btn_DeleteUser
            // 
            this.btn_DeleteUser.Location = new System.Drawing.Point(96, 394);
            this.btn_DeleteUser.Name = "btn_DeleteUser";
            this.btn_DeleteUser.Size = new System.Drawing.Size(75, 23);
            this.btn_DeleteUser.TabIndex = 43;
            this.btn_DeleteUser.Text = "Remove User";
            this.btn_DeleteUser.UseVisualStyleBackColor = true;
            this.btn_DeleteUser.Click += new System.EventHandler(this.btn_DeleteUser_Click);
            // 
            // btn_disableUser
            // 
            this.btn_disableUser.Location = new System.Drawing.Point(15, 394);
            this.btn_disableUser.Name = "btn_disableUser";
            this.btn_disableUser.Size = new System.Drawing.Size(75, 23);
            this.btn_disableUser.TabIndex = 44;
            this.btn_disableUser.Text = "Disbale User";
            this.btn_disableUser.UseVisualStyleBackColor = true;
            this.btn_disableUser.Click += new System.EventHandler(this.btn_disableUser_Click);
            // 
            // btn_getLogin
            // 
            this.btn_getLogin.Location = new System.Drawing.Point(96, 146);
            this.btn_getLogin.Name = "btn_getLogin";
            this.btn_getLogin.Size = new System.Drawing.Size(75, 23);
            this.btn_getLogin.TabIndex = 45;
            this.btn_getLogin.Text = "Get Logins";
            this.btn_getLogin.UseVisualStyleBackColor = true;
            this.btn_getLogin.Click += new System.EventHandler(this.btn_getLogin_Click);
            // 
            // btn_DisbaleLogin
            // 
            this.btn_DisbaleLogin.Location = new System.Drawing.Point(177, 394);
            this.btn_DisbaleLogin.Name = "btn_DisbaleLogin";
            this.btn_DisbaleLogin.Size = new System.Drawing.Size(82, 23);
            this.btn_DisbaleLogin.TabIndex = 47;
            this.btn_DisbaleLogin.Text = "Disbale Login";
            this.btn_DisbaleLogin.UseVisualStyleBackColor = true;
            this.btn_DisbaleLogin.Click += new System.EventHandler(this.btn_DisbaleLogin_Click);
            // 
            // btn_DeleteLogin
            // 
            this.btn_DeleteLogin.Location = new System.Drawing.Point(265, 394);
            this.btn_DeleteLogin.Name = "btn_DeleteLogin";
            this.btn_DeleteLogin.Size = new System.Drawing.Size(75, 23);
            this.btn_DeleteLogin.TabIndex = 48;
            this.btn_DeleteLogin.Text = "Delete Login";
            this.btn_DeleteLogin.UseVisualStyleBackColor = true;
            this.btn_DeleteLogin.Click += new System.EventHandler(this.btn_DeleteLogin_Click);
            // 
            // btn_ResetForm
            // 
            this.btn_ResetForm.Location = new System.Drawing.Point(15, 60);
            this.btn_ResetForm.Name = "btn_ResetForm";
            this.btn_ResetForm.Size = new System.Drawing.Size(156, 23);
            this.btn_ResetForm.TabIndex = 49;
            this.btn_ResetForm.Text = "Disconnect";
            this.btn_ResetForm.UseVisualStyleBackColor = true;
            this.btn_ResetForm.Click += new System.EventHandler(this.btn_ResetForm_Click);
            // 
            // btn_Reset
            // 
            this.btn_Reset.Location = new System.Drawing.Point(516, 107);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(75, 23);
            this.btn_Reset.TabIndex = 50;
            this.btn_Reset.Text = "Reset Vault";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_Reset);
            this.Controls.Add(this.btn_ResetForm);
            this.Controls.Add(this.btn_DeleteLogin);
            this.Controls.Add(this.btn_DisbaleLogin);
            this.Controls.Add(this.btn_getLogin);
            this.Controls.Add(this.btn_disableUser);
            this.Controls.Add(this.btn_DeleteUser);
            this.Controls.Add(this.txt_VaultUsers);
            this.Controls.Add(this.btn_ListUsers);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.hostTextBox);
            this.Controls.Add(this.hostLabel);
            this.Controls.Add(this.vaultComboBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "User Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.Label hostLabel;
        private System.Windows.Forms.ComboBox vaultComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_ListUsers;
        private System.Windows.Forms.TextBox txt_VaultUsers;
        private System.Windows.Forms.Button btn_DeleteUser;
        private System.Windows.Forms.Button btn_disableUser;
        private System.Windows.Forms.Button btn_getLogin;
        private System.Windows.Forms.Button btn_DisbaleLogin;
        private System.Windows.Forms.Button btn_DeleteLogin;
        private System.Windows.Forms.Button btn_ResetForm;
        private System.Windows.Forms.Button btn_Reset;
    }
}

