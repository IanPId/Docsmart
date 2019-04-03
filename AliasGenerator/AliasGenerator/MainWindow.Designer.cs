namespace AliasGenerator
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param elementName="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.vaultComboBox = new System.Windows.Forms.ComboBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.startButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.otPrefixTextBox = new System.Windows.Forms.TextBox();
            this.clPrefixTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pdPrefixTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.vlPrefixTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.wfPrefixTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.wfsPrefixTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.hostLabel = new System.Windows.Forms.Label();
            this.hostTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.otCheckBox = new System.Windows.Forms.CheckBox();
            this.clCheckBox = new System.Windows.Forms.CheckBox();
            this.pdCheckBox = new System.Windows.Forms.CheckBox();
            this.vlCheckBox = new System.Windows.Forms.CheckBox();
            this.wfCheckBox = new System.Windows.Forms.CheckBox();
            this.wfsCheckBox = new System.Windows.Forms.CheckBox();
            this.clearSelectedButton = new System.Windows.Forms.Button();
            this.clearAllButton = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.aliasesClassCheckBox = new System.Windows.Forms.CheckBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.configClassCheckBox = new System.Windows.Forms.CheckBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.aliasesClassPathTextBox = new System.Windows.Forms.TextBox();
            this.configClassPathTextBox = new System.Windows.Forms.TextBox();
            this.aliasesClassNameTextBox = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.configClassNameTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Target vault:";
            // 
            // vaultComboBox
            // 
            this.vaultComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vaultComboBox.Enabled = false;
            this.vaultComboBox.FormattingEnabled = true;
            this.vaultComboBox.Location = new System.Drawing.Point(206, 142);
            this.vaultComboBox.Name = "vaultComboBox";
            this.vaultComboBox.Size = new System.Drawing.Size(268, 21);
            this.vaultComboBox.TabIndex = 3;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // startButton
            // 
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(30, 443);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(191, 23);
            this.startButton.TabIndex = 12;
            this.startButton.Text = "Create aliases";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Object type alias prefix:";
            // 
            // otPrefixTextBox
            // 
            this.otPrefixTextBox.Location = new System.Drawing.Point(206, 192);
            this.otPrefixTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.otPrefixTextBox.Name = "otPrefixTextBox";
            this.otPrefixTextBox.Size = new System.Drawing.Size(268, 20);
            this.otPrefixTextBox.TabIndex = 14;
            this.otPrefixTextBox.Text = "Obj.";
            this.otPrefixTextBox.TextChanged += new System.EventHandler(this.otPrefixTextBox_TextChanged);
            // 
            // clPrefixTextBox
            // 
            this.clPrefixTextBox.Location = new System.Drawing.Point(206, 227);
            this.clPrefixTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.clPrefixTextBox.Name = "clPrefixTextBox";
            this.clPrefixTextBox.Size = new System.Drawing.Size(268, 20);
            this.clPrefixTextBox.TabIndex = 16;
            this.clPrefixTextBox.Text = "Class.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 229);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Class alias prefix:";
            // 
            // pdPrefixTextBox
            // 
            this.pdPrefixTextBox.Location = new System.Drawing.Point(206, 264);
            this.pdPrefixTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pdPrefixTextBox.Name = "pdPrefixTextBox";
            this.pdPrefixTextBox.Size = new System.Drawing.Size(268, 20);
            this.pdPrefixTextBox.TabIndex = 18;
            this.pdPrefixTextBox.Text = "Prop.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 266);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Property def alias prefix:";
            // 
            // vlPrefixTextBox
            // 
            this.vlPrefixTextBox.Location = new System.Drawing.Point(206, 300);
            this.vlPrefixTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.vlPrefixTextBox.Name = "vlPrefixTextBox";
            this.vlPrefixTextBox.Size = new System.Drawing.Size(268, 20);
            this.vlPrefixTextBox.TabIndex = 20;
            this.vlPrefixTextBox.Text = "VL.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(49, 302);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Value list alias prefix:";
            // 
            // wfPrefixTextBox
            // 
            this.wfPrefixTextBox.Location = new System.Drawing.Point(206, 337);
            this.wfPrefixTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.wfPrefixTextBox.Name = "wfPrefixTextBox";
            this.wfPrefixTextBox.Size = new System.Drawing.Size(268, 20);
            this.wfPrefixTextBox.TabIndex = 22;
            this.wfPrefixTextBox.Text = "WF.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(49, 339);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Workflow alias prefix:";
            // 
            // wfsPrefixTextBox
            // 
            this.wfsPrefixTextBox.Location = new System.Drawing.Point(206, 375);
            this.wfsPrefixTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.wfsPrefixTextBox.Name = "wfsPrefixTextBox";
            this.wfsPrefixTextBox.Size = new System.Drawing.Size(268, 20);
            this.wfsPrefixTextBox.TabIndex = 24;
            this.wfsPrefixTextBox.Text = "WF.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(49, 377);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Workflow state alias prefix:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(503, 194);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(161, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Example result: Obj.Customer";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(503, 227);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(159, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Example result: Class.Customer";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(503, 264);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(161, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "Example result: Prop.Customer";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(503, 302);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(324, 13);
            this.label11.TabIndex = 28;
            this.label11.Text = "Example result: VL.Cities (note: uses the plural form of VL name)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(503, 337);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(201, 13);
            this.label12.TabIndex = 29;
            this.label12.Text = "Example result: WF.Invoice_approval";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(503, 377);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(312, 13);
            this.label13.TabIndex = 30;
            this.label13.Text = "Example result: WF.Invoice_approval.Waiting_for_approval";
            // 
            // hostLabel
            // 
            this.hostLabel.AutoSize = true;
            this.hostLabel.Location = new System.Drawing.Point(27, 71);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Size = new System.Drawing.Size(64, 13);
            this.hostLabel.TabIndex = 31;
            this.hostLabel.Text = "Server host:";
            // 
            // hostTextBox
            // 
            this.hostTextBox.Location = new System.Drawing.Point(206, 69);
            this.hostTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.hostTextBox.Name = "hostTextBox";
            this.hostTextBox.Size = new System.Drawing.Size(268, 20);
            this.hostTextBox.TabIndex = 0;
            this.hostTextBox.Text = "localhost";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(203, 97);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(473, 13);
            this.label14.TabIndex = 33;
            this.label14.Text = "(Connection defaults: TCP/IP, port 2266, current Windows user. Modify app soure c" +
    "ode if needed.)";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(30, 93);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(155, 23);
            this.connectButton.TabIndex = 34;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // otCheckBox
            // 
            this.otCheckBox.AutoSize = true;
            this.otCheckBox.Checked = true;
            this.otCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.otCheckBox.Location = new System.Drawing.Point(30, 192);
            this.otCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.otCheckBox.Name = "otCheckBox";
            this.otCheckBox.Size = new System.Drawing.Size(15, 14);
            this.otCheckBox.TabIndex = 35;
            this.otCheckBox.UseVisualStyleBackColor = true;
            // 
            // clCheckBox
            // 
            this.clCheckBox.AutoSize = true;
            this.clCheckBox.Checked = true;
            this.clCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.clCheckBox.Location = new System.Drawing.Point(30, 227);
            this.clCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.clCheckBox.Name = "clCheckBox";
            this.clCheckBox.Size = new System.Drawing.Size(15, 14);
            this.clCheckBox.TabIndex = 36;
            this.clCheckBox.UseVisualStyleBackColor = true;
            // 
            // pdCheckBox
            // 
            this.pdCheckBox.AutoSize = true;
            this.pdCheckBox.Checked = true;
            this.pdCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pdCheckBox.Location = new System.Drawing.Point(30, 264);
            this.pdCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pdCheckBox.Name = "pdCheckBox";
            this.pdCheckBox.Size = new System.Drawing.Size(15, 14);
            this.pdCheckBox.TabIndex = 37;
            this.pdCheckBox.UseVisualStyleBackColor = true;
            // 
            // vlCheckBox
            // 
            this.vlCheckBox.AutoSize = true;
            this.vlCheckBox.Checked = true;
            this.vlCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.vlCheckBox.Location = new System.Drawing.Point(30, 300);
            this.vlCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.vlCheckBox.Name = "vlCheckBox";
            this.vlCheckBox.Size = new System.Drawing.Size(15, 14);
            this.vlCheckBox.TabIndex = 38;
            this.vlCheckBox.UseVisualStyleBackColor = true;
            // 
            // wfCheckBox
            // 
            this.wfCheckBox.AutoSize = true;
            this.wfCheckBox.Checked = true;
            this.wfCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.wfCheckBox.Location = new System.Drawing.Point(30, 337);
            this.wfCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.wfCheckBox.Name = "wfCheckBox";
            this.wfCheckBox.Size = new System.Drawing.Size(15, 14);
            this.wfCheckBox.TabIndex = 39;
            this.wfCheckBox.UseVisualStyleBackColor = true;
            // 
            // wfsCheckBox
            // 
            this.wfsCheckBox.AutoSize = true;
            this.wfsCheckBox.Checked = true;
            this.wfsCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.wfsCheckBox.Location = new System.Drawing.Point(30, 375);
            this.wfsCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.wfsCheckBox.Name = "wfsCheckBox";
            this.wfsCheckBox.Size = new System.Drawing.Size(15, 14);
            this.wfsCheckBox.TabIndex = 40;
            this.wfsCheckBox.UseVisualStyleBackColor = true;
            // 
            // clearSelectedButton
            // 
            this.clearSelectedButton.Enabled = false;
            this.clearSelectedButton.Location = new System.Drawing.Point(315, 443);
            this.clearSelectedButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.clearSelectedButton.Name = "clearSelectedButton";
            this.clearSelectedButton.Size = new System.Drawing.Size(281, 23);
            this.clearSelectedButton.TabIndex = 41;
            this.clearSelectedButton.Text = "Clear aliases of the specified format";
            this.clearSelectedButton.UseVisualStyleBackColor = true;
            this.clearSelectedButton.Click += new System.EventHandler(this.clearSelectedButton_Click);
            // 
            // clearAllButton
            // 
            this.clearAllButton.Enabled = false;
            this.clearAllButton.Location = new System.Drawing.Point(619, 443);
            this.clearAllButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.clearAllButton.Name = "clearAllButton";
            this.clearAllButton.Size = new System.Drawing.Size(211, 23);
            this.clearAllButton.TabIndex = 42;
            this.clearAllButton.Text = "!!! Clear all aliases in the vault !!!";
            this.clearAllButton.UseVisualStyleBackColor = true;
            this.clearAllButton.Click += new System.EventHandler(this.clearAllButton_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(27, 416);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(252, 13);
            this.label15.TabIndex = 43;
            this.label15.Text = "For the metadata elements selected above:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(27, 12);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(38, 13);
            this.label16.TabIndex = 44;
            this.label16.Text = "Note!";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(91, 12);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(714, 13);
            this.label17.TabIndex = 45;
            this.label17.Text = "This tool has NOT been fully tested and will NOT be officially supported in any w" +
    "ay. Does not check for duplicate aliases in case there are e.g. multiple";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(256, 34);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(465, 13);
            this.label18.TabIndex = 46;
            this.label18.Text = "Use at your own risk, please inspect and modify the source code as necessary to s" +
    "uit your needs.";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(91, 34);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(160, 13);
            this.label19.TabIndex = 47;
            this.label19.Text = "property defs by the same name.";
            // 
            // aliasesClassCheckBox
            // 
            this.aliasesClassCheckBox.AutoSize = true;
            this.aliasesClassCheckBox.Checked = true;
            this.aliasesClassCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.aliasesClassCheckBox.Location = new System.Drawing.Point(30, 504);
            this.aliasesClassCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.aliasesClassCheckBox.Name = "aliasesClassCheckBox";
            this.aliasesClassCheckBox.Size = new System.Drawing.Size(15, 14);
            this.aliasesClassCheckBox.TabIndex = 49;
            this.aliasesClassCheckBox.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(49, 506);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(51, 13);
            this.label20.TabIndex = 48;
            this.label20.Text = "Generate";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(27, 480);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(165, 13);
            this.label21.TabIndex = 50;
            this.label21.Text = "Also, while creating aliases:";
            // 
            // configClassCheckBox
            // 
            this.configClassCheckBox.AutoSize = true;
            this.configClassCheckBox.Checked = true;
            this.configClassCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.configClassCheckBox.Location = new System.Drawing.Point(30, 535);
            this.configClassCheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.configClassCheckBox.Name = "configClassCheckBox";
            this.configClassCheckBox.Size = new System.Drawing.Size(15, 14);
            this.configClassCheckBox.TabIndex = 52;
            this.configClassCheckBox.UseVisualStyleBackColor = true;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(49, 537);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(51, 13);
            this.label22.TabIndex = 51;
            this.label22.Text = "Generate";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(452, 506);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(70, 13);
            this.label23.TabIndex = 53;
            this.label23.Text = "Target folder:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(452, 537);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(70, 13);
            this.label24.TabIndex = 54;
            this.label24.Text = "Target folder:";
            // 
            // aliasesClassPathTextBox
            // 
            this.aliasesClassPathTextBox.Location = new System.Drawing.Point(540, 504);
            this.aliasesClassPathTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.aliasesClassPathTextBox.Name = "aliasesClassPathTextBox";
            this.aliasesClassPathTextBox.Size = new System.Drawing.Size(158, 20);
            this.aliasesClassPathTextBox.TabIndex = 55;
            this.aliasesClassPathTextBox.Text = "C:\\temp";
            // 
            // configClassPathTextBox
            // 
            this.configClassPathTextBox.Location = new System.Drawing.Point(540, 535);
            this.configClassPathTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.configClassPathTextBox.Name = "configClassPathTextBox";
            this.configClassPathTextBox.Size = new System.Drawing.Size(158, 20);
            this.configClassPathTextBox.TabIndex = 56;
            this.configClassPathTextBox.Text = "C:\\temp";
            // 
            // aliasesClassNameTextBox
            // 
            this.aliasesClassNameTextBox.Location = new System.Drawing.Point(101, 504);
            this.aliasesClassNameTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.aliasesClassNameTextBox.Name = "aliasesClassNameTextBox";
            this.aliasesClassNameTextBox.Size = new System.Drawing.Size(64, 20);
            this.aliasesClassNameTextBox.TabIndex = 57;
            this.aliasesClassNameTextBox.Text = "Aliases";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(162, 506);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(265, 13);
            this.label25.TabIndex = 58;
            this.label25.Text = ".cs with alias string constants for your vault application.";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(162, 537);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(227, 13);
            this.label26.TabIndex = 59;
            this.label26.Text = ".cs with MFIdentifiers for your vault application.";
            // 
            // configClassNameTextBox
            // 
            this.configClassNameTextBox.Location = new System.Drawing.Point(101, 535);
            this.configClassNameTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.configClassNameTextBox.Name = "configClassNameTextBox";
            this.configClassNameTextBox.Size = new System.Drawing.Size(64, 20);
            this.configClassNameTextBox.TabIndex = 60;
            this.configClassNameTextBox.Text = "Config";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(849, 580);
            this.Controls.Add(this.configClassNameTextBox);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.aliasesClassNameTextBox);
            this.Controls.Add(this.configClassPathTextBox);
            this.Controls.Add(this.aliasesClassPathTextBox);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.configClassCheckBox);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.aliasesClassCheckBox);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.clearAllButton);
            this.Controls.Add(this.clearSelectedButton);
            this.Controls.Add(this.wfsCheckBox);
            this.Controls.Add(this.wfCheckBox);
            this.Controls.Add(this.vlCheckBox);
            this.Controls.Add(this.pdCheckBox);
            this.Controls.Add(this.clCheckBox);
            this.Controls.Add(this.otCheckBox);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.hostTextBox);
            this.Controls.Add(this.hostLabel);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.wfsPrefixTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.wfPrefixTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.vlPrefixTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pdPrefixTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.clPrefixTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.otPrefixTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.vaultComboBox);
            this.Controls.Add(this.label1);
            this.Name = "MainWindow";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 13);
            this.Text = "Alias Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox vaultComboBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox otPrefixTextBox;
        private System.Windows.Forms.TextBox clPrefixTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pdPrefixTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox vlPrefixTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox wfPrefixTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox wfsPrefixTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label hostLabel;
        private System.Windows.Forms.TextBox hostTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.CheckBox otCheckBox;
        private System.Windows.Forms.CheckBox clCheckBox;
        private System.Windows.Forms.CheckBox pdCheckBox;
        private System.Windows.Forms.CheckBox vlCheckBox;
        private System.Windows.Forms.CheckBox wfCheckBox;
        private System.Windows.Forms.CheckBox wfsCheckBox;
        private System.Windows.Forms.Button clearSelectedButton;
        private System.Windows.Forms.Button clearAllButton;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.CheckBox aliasesClassCheckBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckBox configClassCheckBox;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox aliasesClassPathTextBox;
        private System.Windows.Forms.TextBox configClassPathTextBox;
        private System.Windows.Forms.TextBox aliasesClassNameTextBox;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox configClassNameTextBox;
    }
}