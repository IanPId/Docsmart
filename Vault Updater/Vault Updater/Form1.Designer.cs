namespace Vault_Updater
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
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.hostLabel = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.vaultComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetProperties = new System.Windows.Forms.Button();
            this.propertyComboBox = new System.Windows.Forms.ComboBox();
            this.btnAddToClass = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // hostTextBox
            // 
            this.hostTextBox.Location = new System.Drawing.Point(190, 20);
            this.hostTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(268, 20);
            this.hostTextBox.TabIndex = 32;
            this.hostTextBox.Text = "localhost";
            // 
            // hostLabel
            // 
            this.hostLabel.AutoSize = true;
            this.hostLabel.Location = new System.Drawing.Point(11, 22);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Size = new System.Drawing.Size(64, 13);
            this.hostLabel.TabIndex = 33;
            this.hostLabel.Text = "Server host:";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(14, 48);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(155, 23);
            this.connectButton.TabIndex = 38;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(187, 52);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(473, 13);
            this.label14.TabIndex = 37;
            this.label14.Text = "(Connection defaults: TCP/IP, port 2266, current Windows user. Modify app soure c" +
    "ode if needed.)";
            // 
            // vaultComboBox
            // 
            this.vaultComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vaultComboBox.Enabled = false;
            this.vaultComboBox.FormattingEnabled = true;
            this.vaultComboBox.Location = new System.Drawing.Point(190, 97);
            this.vaultComboBox.Name = "vaultComboBox";
            this.vaultComboBox.Size = new System.Drawing.Size(268, 21);
            this.vaultComboBox.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "Target vault:";
            // 
            // btnGetProperties
            // 
            this.btnGetProperties.Location = new System.Drawing.Point(14, 134);
            this.btnGetProperties.Name = "btnGetProperties";
            this.btnGetProperties.Size = new System.Drawing.Size(155, 23);
            this.btnGetProperties.TabIndex = 39;
            this.btnGetProperties.Text = "Get properties";
            this.btnGetProperties.UseVisualStyleBackColor = true;
            this.btnGetProperties.Click += new System.EventHandler(this.button1_Click);
            // 
            // propertyComboBox
            // 
            this.propertyComboBox.FormattingEnabled = true;
            this.propertyComboBox.Location = new System.Drawing.Point(190, 178);
            this.propertyComboBox.Name = "propertyComboBox";
            this.propertyComboBox.Size = new System.Drawing.Size(268, 21);
            this.propertyComboBox.TabIndex = 40;
            
            // 
            // btnAddToClass
            // 
            this.btnAddToClass.Location = new System.Drawing.Point(14, 208);
            this.btnAddToClass.Name = "btnAddToClass";
            this.btnAddToClass.Size = new System.Drawing.Size(155, 23);
            this.btnAddToClass.TabIndex = 41;
            this.btnAddToClass.Text = "Add to Doc Classes";
            this.btnAddToClass.UseVisualStyleBackColor = true;
            this.btnAddToClass.Click += new System.EventHandler(this.btnAddToClass_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 450);
            this.Controls.Add(this.btnAddToClass);
            this.Controls.Add(this.propertyComboBox);
            this.Controls.Add(this.btnGetProperties);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.vaultComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hostTextBox);
            this.Controls.Add(this.hostLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.Label hostLabel;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox vaultComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetProperties;
        private System.Windows.Forms.ComboBox propertyComboBox;
        private System.Windows.Forms.Button btnAddToClass;
    }
}

