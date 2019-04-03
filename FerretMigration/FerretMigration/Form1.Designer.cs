namespace FerretMigration
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.vaultComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.hostLabel = new System.Windows.Forms.Label();
            this.sqlHostTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.sqlConnectButton = new System.Windows.Forms.Button();
            this.sqlDatabaseComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.startMigrationButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txtStartingRecord = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.singleRecordTextBox = new System.Windows.Forms.TextBox();
            this.singleRecordCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ferret SQL Connection Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "M-Fiels Vault Connection Settings";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(18, 218);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(155, 23);
            this.connectButton.TabIndex = 44;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(191, 222);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(473, 13);
            this.label14.TabIndex = 43;
            this.label14.Text = "(Connection defaults: TCP/IP, port 2266, current Windows user. Modify app soure c" +
    "ode if needed.)";
            // 
            // vaultComboBox
            // 
            this.vaultComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vaultComboBox.Enabled = false;
            this.vaultComboBox.FormattingEnabled = true;
            this.vaultComboBox.Location = new System.Drawing.Point(194, 267);
            this.vaultComboBox.Name = "vaultComboBox";
            this.vaultComboBox.Size = new System.Drawing.Size(268, 21);
            this.vaultComboBox.TabIndex = 42;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 269);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 41;
            this.label3.Text = "Target vault:";
            // 
            // hostTextBox
            // 
            this.hostTextBox.Location = new System.Drawing.Point(194, 190);
            this.hostTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(268, 20);
            this.hostTextBox.TabIndex = 39;
            this.hostTextBox.Text = "localhost";
            // 
            // hostLabel
            // 
            this.hostLabel.AutoSize = true;
            this.hostLabel.Location = new System.Drawing.Point(15, 192);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Size = new System.Drawing.Size(64, 13);
            this.hostLabel.TabIndex = 40;
            this.hostLabel.Text = "Server host:";
            // 
            // sqlHostTextBox
            // 
            this.sqlHostTextBox.Location = new System.Drawing.Point(191, 45);
            this.sqlHostTextBox.Name = "sqlHostTextBox";
            this.sqlHostTextBox.Size = new System.Drawing.Size(268, 20);
            this.sqlHostTextBox.TabIndex = 45;
            this.sqlHostTextBox.Text = "localhost";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 46;
            this.label4.Text = "Server host:";
            // 
            // sqlConnectButton
            // 
            this.sqlConnectButton.Location = new System.Drawing.Point(15, 76);
            this.sqlConnectButton.Name = "sqlConnectButton";
            this.sqlConnectButton.Size = new System.Drawing.Size(155, 23);
            this.sqlConnectButton.TabIndex = 47;
            this.sqlConnectButton.Text = "Connect";
            this.sqlConnectButton.UseVisualStyleBackColor = true;
            this.sqlConnectButton.Click += new System.EventHandler(this.sqlConnectButton_Click);
            // 
            // sqlDatabaseComboBox
            // 
            this.sqlDatabaseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sqlDatabaseComboBox.FormattingEnabled = true;
            this.sqlDatabaseComboBox.Location = new System.Drawing.Point(191, 115);
            this.sqlDatabaseComboBox.Name = "sqlDatabaseComboBox";
            this.sqlDatabaseComboBox.Size = new System.Drawing.Size(268, 21);
            this.sqlDatabaseComboBox.TabIndex = 48;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 49;
            this.label5.Text = "Target database";
            // 
            // startMigrationButton
            // 
            this.startMigrationButton.Location = new System.Drawing.Point(18, 322);
            this.startMigrationButton.Name = "startMigrationButton";
            this.startMigrationButton.Size = new System.Drawing.Size(155, 23);
            this.startMigrationButton.TabIndex = 50;
            this.startMigrationButton.Text = "Start Migration";
            this.startMigrationButton.UseVisualStyleBackColor = true;
            this.startMigrationButton.Click += new System.EventHandler(this.startMigrationButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 351);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(770, 23);
            this.progressBar1.TabIndex = 51;
            // 
            // txtStartingRecord
            // 
            this.txtStartingRecord.Location = new System.Drawing.Point(271, 322);
            this.txtStartingRecord.Name = "txtStartingRecord";
            this.txtStartingRecord.Size = new System.Drawing.Size(100, 20);
            this.txtStartingRecord.TabIndex = 52;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(191, 327);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 53;
            this.label6.Text = "Start at record";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(420, 327);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 55;
            this.label7.Text = "Single Record";
            this.label7.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // singleRecordTextBox
            // 
            this.singleRecordTextBox.Location = new System.Drawing.Point(500, 322);
            this.singleRecordTextBox.Name = "singleRecordTextBox";
            this.singleRecordTextBox.Size = new System.Drawing.Size(100, 20);
            this.singleRecordTextBox.TabIndex = 54;
            // 
            // singleRecordCheckbox
            // 
            this.singleRecordCheckbox.AutoSize = true;
            this.singleRecordCheckbox.Location = new System.Drawing.Point(607, 322);
            this.singleRecordCheckbox.Name = "singleRecordCheckbox";
            this.singleRecordCheckbox.Size = new System.Drawing.Size(117, 17);
            this.singleRecordCheckbox.TabIndex = 56;
            this.singleRecordCheckbox.Text = "Single Record Only";
            this.singleRecordCheckbox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.singleRecordCheckbox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.singleRecordTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtStartingRecord);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.startMigrationButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.sqlDatabaseComboBox);
            this.Controls.Add(this.sqlConnectButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.sqlHostTextBox);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.vaultComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hostTextBox);
            this.Controls.Add(this.hostLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox vaultComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.Label hostLabel;
        private System.Windows.Forms.TextBox sqlHostTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button sqlConnectButton;
        private System.Windows.Forms.ComboBox sqlDatabaseComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button startMigrationButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txtStartingRecord;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox singleRecordTextBox;
        private System.Windows.Forms.CheckBox singleRecordCheckbox;
    }
}

