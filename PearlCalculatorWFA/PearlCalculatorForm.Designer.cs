
namespace PearlCalculatorWFA
{
    partial class PearlCalculatorWFA
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GeneralFTLInputTabPage = new System.Windows.Forms.TabPage();
            this.GeneralFTLTabControl = new System.Windows.Forms.TabControl();
            this.GeneralFTLGeneralTabPage = new System.Windows.Forms.TabPage();
            this.SaveSettingButton = new System.Windows.Forms.Button();
            this.ImportSettingButton = new System.Windows.Forms.Button();
            this.PearlSimulateButton = new System.Windows.Forms.Button();
            this.CalculateTNTButton = new System.Windows.Forms.Button();
            this.BlueTNTTextBox = new System.Windows.Forms.TextBox();
            this.BlueTNTLabel = new System.Windows.Forms.Label();
            this.RedTNTTextBox = new System.Windows.Forms.TextBox();
            this.RedTNTLabel = new System.Windows.Forms.Label();
            this.DirectionRadioGroupBox = new System.Windows.Forms.GroupBox();
            this.WestRadioButton = new System.Windows.Forms.RadioButton();
            this.EastRadioButton = new System.Windows.Forms.RadioButton();
            this.SouthRadioButton = new System.Windows.Forms.RadioButton();
            this.NorthRadioButton = new System.Windows.Forms.RadioButton();
            this.MaxTNTTextBox = new System.Windows.Forms.TextBox();
            this.MaxTNTLabel = new System.Windows.Forms.Label();
            this.DestinationZTextBox = new System.Windows.Forms.TextBox();
            this.DestinationZLabel = new System.Windows.Forms.Label();
            this.DestinationXTextBox = new System.Windows.Forms.TextBox();
            this.DestinationXLabel = new System.Windows.Forms.Label();
            this.PearlZTextBox = new System.Windows.Forms.TextBox();
            this.PearlZLabel = new System.Windows.Forms.Label();
            this.PearlXTextBox = new System.Windows.Forms.TextBox();
            this.PearlXLabel = new System.Windows.Forms.Label();
            this.GeneralFTLAdvancedTabPage = new System.Windows.Forms.TabPage();
            this.TNTWeightGroupBox = new System.Windows.Forms.GroupBox();
            this.DistanceRadioButton = new System.Windows.Forms.RadioButton();
            this.TNTRadioButton = new System.Windows.Forms.RadioButton();
            this.DistanceWeightRadioButton = new System.Windows.Forms.RadioButton();
            this.TNTWeightTrackerSlider = new System.Windows.Forms.TrackBar();
            this.OffsetGroupBox = new System.Windows.Forms.GroupBox();
            this.OffsetZTextBox = new System.Windows.Forms.TextBox();
            this.ZOffsetLabel = new System.Windows.Forms.Label();
            this.OffsetXTextBox = new System.Windows.Forms.TextBox();
            this.XOffsetLabel = new System.Windows.Forms.Label();
            this.GeneralFTLSettingsTabPage = new System.Windows.Forms.TabPage();
            this.SettingInputTextBox = new System.Windows.Forms.TextBox();
            this.ChangeSettingButton = new System.Windows.Forms.Button();
            this.ResetSettingButton = new System.Windows.Forms.Button();
            this.SettingListView = new System.Windows.Forms.ListView();
            this.InputTabControl = new System.Windows.Forms.TabControl();
            this.BasicInputOutputSystem = new System.Windows.Forms.TabControl();
            this.OutputTabPage = new System.Windows.Forms.TabPage();
            this.BasicDirectionOutSystem = new System.Windows.Forms.ListView();
            this.BasicOutputSystem = new System.Windows.Forms.ListView();
            this.InputTabPage = new System.Windows.Forms.TabPage();
            this.ConsoleInputTextBox = new System.Windows.Forms.TextBox();
            this.ConsoleEnterButton = new System.Windows.Forms.Button();
            this.ConsoleListView = new System.Windows.Forms.ListView();
            this.GeneralFTLInputTabPage.SuspendLayout();
            this.GeneralFTLTabControl.SuspendLayout();
            this.GeneralFTLGeneralTabPage.SuspendLayout();
            this.DirectionRadioGroupBox.SuspendLayout();
            this.GeneralFTLAdvancedTabPage.SuspendLayout();
            this.TNTWeightGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TNTWeightTrackerSlider)).BeginInit();
            this.OffsetGroupBox.SuspendLayout();
            this.GeneralFTLSettingsTabPage.SuspendLayout();
            this.InputTabControl.SuspendLayout();
            this.BasicInputOutputSystem.SuspendLayout();
            this.OutputTabPage.SuspendLayout();
            this.InputTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // GeneralFTLInputTabPage
            // 
            this.GeneralFTLInputTabPage.Controls.Add(this.GeneralFTLTabControl);
            this.GeneralFTLInputTabPage.Location = new System.Drawing.Point(4, 26);
            this.GeneralFTLInputTabPage.Name = "GeneralFTLInputTabPage";
            this.GeneralFTLInputTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralFTLInputTabPage.Size = new System.Drawing.Size(381, 591);
            this.GeneralFTLInputTabPage.TabIndex = 0;
            this.GeneralFTLInputTabPage.Text = "General FTL";
            this.GeneralFTLInputTabPage.UseVisualStyleBackColor = true;
            // 
            // GeneralFTLTabControl
            // 
            this.GeneralFTLTabControl.Controls.Add(this.GeneralFTLGeneralTabPage);
            this.GeneralFTLTabControl.Controls.Add(this.GeneralFTLAdvancedTabPage);
            this.GeneralFTLTabControl.Controls.Add(this.GeneralFTLSettingsTabPage);
            this.GeneralFTLTabControl.Location = new System.Drawing.Point(4, 5);
            this.GeneralFTLTabControl.Name = "GeneralFTLTabControl";
            this.GeneralFTLTabControl.SelectedIndex = 0;
            this.GeneralFTLTabControl.Size = new System.Drawing.Size(381, 587);
            this.GeneralFTLTabControl.TabIndex = 0;
            this.GeneralFTLTabControl.SelectedIndexChanged += new System.EventHandler(this.GeneralFTLTabControl_SelectedIndexChanged);
            // 
            // GeneralFTLGeneralTabPage
            // 
            this.GeneralFTLGeneralTabPage.Controls.Add(this.SaveSettingButton);
            this.GeneralFTLGeneralTabPage.Controls.Add(this.ImportSettingButton);
            this.GeneralFTLGeneralTabPage.Controls.Add(this.PearlSimulateButton);
            this.GeneralFTLGeneralTabPage.Controls.Add(this.CalculateTNTButton);
            this.GeneralFTLGeneralTabPage.Controls.Add(this.BlueTNTTextBox);
            this.GeneralFTLGeneralTabPage.Controls.Add(this.BlueTNTLabel);
            this.GeneralFTLGeneralTabPage.Controls.Add(this.RedTNTTextBox);
            this.GeneralFTLGeneralTabPage.Controls.Add(this.RedTNTLabel);
            this.GeneralFTLGeneralTabPage.Controls.Add(this.DirectionRadioGroupBox);
            this.GeneralFTLGeneralTabPage.Controls.Add(this.MaxTNTTextBox);
            this.GeneralFTLGeneralTabPage.Controls.Add(this.MaxTNTLabel);
            this.GeneralFTLGeneralTabPage.Controls.Add(this.DestinationZTextBox);
            this.GeneralFTLGeneralTabPage.Controls.Add(this.DestinationZLabel);
            this.GeneralFTLGeneralTabPage.Controls.Add(this.DestinationXTextBox);
            this.GeneralFTLGeneralTabPage.Controls.Add(this.DestinationXLabel);
            this.GeneralFTLGeneralTabPage.Controls.Add(this.PearlZTextBox);
            this.GeneralFTLGeneralTabPage.Controls.Add(this.PearlZLabel);
            this.GeneralFTLGeneralTabPage.Controls.Add(this.PearlXTextBox);
            this.GeneralFTLGeneralTabPage.Controls.Add(this.PearlXLabel);
            this.GeneralFTLGeneralTabPage.Location = new System.Drawing.Point(4, 26);
            this.GeneralFTLGeneralTabPage.Name = "GeneralFTLGeneralTabPage";
            this.GeneralFTLGeneralTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralFTLGeneralTabPage.Size = new System.Drawing.Size(373, 557);
            this.GeneralFTLGeneralTabPage.TabIndex = 0;
            this.GeneralFTLGeneralTabPage.Text = "General";
            this.GeneralFTLGeneralTabPage.UseVisualStyleBackColor = true;
            // 
            // SaveSettingButton
            // 
            this.SaveSettingButton.Location = new System.Drawing.Point(4, 525);
            this.SaveSettingButton.Name = "SaveSettingButton";
            this.SaveSettingButton.Size = new System.Drawing.Size(366, 26);
            this.SaveSettingButton.TabIndex = 15;
            this.SaveSettingButton.Text = "Save Setting";
            this.SaveSettingButton.UseVisualStyleBackColor = true;
            this.SaveSettingButton.Click += new System.EventHandler(this.SaveSettingButton_Click);
            // 
            // ImportSettingButton
            // 
            this.ImportSettingButton.Location = new System.Drawing.Point(4, 492);
            this.ImportSettingButton.Name = "ImportSettingButton";
            this.ImportSettingButton.Size = new System.Drawing.Size(366, 26);
            this.ImportSettingButton.TabIndex = 17;
            this.ImportSettingButton.Text = "Import Setting";
            this.ImportSettingButton.UseVisualStyleBackColor = true;
            this.ImportSettingButton.Click += new System.EventHandler(this.ImportSettingButton_Click);
            // 
            // PearlSimulateButton
            // 
            this.PearlSimulateButton.Location = new System.Drawing.Point(4, 459);
            this.PearlSimulateButton.Name = "PearlSimulateButton";
            this.PearlSimulateButton.Size = new System.Drawing.Size(366, 26);
            this.PearlSimulateButton.TabIndex = 15;
            this.PearlSimulateButton.Text = "Pearl Simulate";
            this.PearlSimulateButton.UseVisualStyleBackColor = true;
            this.PearlSimulateButton.Click += new System.EventHandler(this.PearlSimulateButton_Click);
            // 
            // CalculateTNTButton
            // 
            this.CalculateTNTButton.Location = new System.Drawing.Point(4, 426);
            this.CalculateTNTButton.Name = "CalculateTNTButton";
            this.CalculateTNTButton.Size = new System.Drawing.Size(366, 26);
            this.CalculateTNTButton.TabIndex = 14;
            this.CalculateTNTButton.Text = "Calculate TNT Amount";
            this.CalculateTNTButton.UseVisualStyleBackColor = true;
            this.CalculateTNTButton.Click += new System.EventHandler(this.CalculateTNTButton_Click);
            // 
            // BlueTNTTextBox
            // 
            this.BlueTNTTextBox.Location = new System.Drawing.Point(4, 392);
            this.BlueTNTTextBox.Name = "BlueTNTTextBox";
            this.BlueTNTTextBox.Size = new System.Drawing.Size(366, 23);
            this.BlueTNTTextBox.TabIndex = 12;
            this.BlueTNTTextBox.TextChanged += new System.EventHandler(this.BlueTNTTextBox_TextChanged);
            // 
            // BlueTNTLabel
            // 
            this.BlueTNTLabel.AutoSize = true;
            this.BlueTNTLabel.Location = new System.Drawing.Point(4, 371);
            this.BlueTNTLabel.Name = "BlueTNTLabel";
            this.BlueTNTLabel.Size = new System.Drawing.Size(68, 17);
            this.BlueTNTLabel.TabIndex = 13;
            this.BlueTNTLabel.Text = "Blue TNT :";
            // 
            // RedTNTTextBox
            // 
            this.RedTNTTextBox.Location = new System.Drawing.Point(4, 341);
            this.RedTNTTextBox.Name = "RedTNTTextBox";
            this.RedTNTTextBox.Size = new System.Drawing.Size(366, 23);
            this.RedTNTTextBox.TabIndex = 10;
            this.RedTNTTextBox.TextChanged += new System.EventHandler(this.RedTNTTextBox_TextChanged);
            // 
            // RedTNTLabel
            // 
            this.RedTNTLabel.AutoSize = true;
            this.RedTNTLabel.Location = new System.Drawing.Point(4, 320);
            this.RedTNTLabel.Name = "RedTNTLabel";
            this.RedTNTLabel.Size = new System.Drawing.Size(66, 17);
            this.RedTNTLabel.TabIndex = 11;
            this.RedTNTLabel.Text = "Red TNT :";
            // 
            // DirectionRadioGroupBox
            // 
            this.DirectionRadioGroupBox.Controls.Add(this.WestRadioButton);
            this.DirectionRadioGroupBox.Controls.Add(this.EastRadioButton);
            this.DirectionRadioGroupBox.Controls.Add(this.SouthRadioButton);
            this.DirectionRadioGroupBox.Controls.Add(this.NorthRadioButton);
            this.DirectionRadioGroupBox.Location = new System.Drawing.Point(0, 264);
            this.DirectionRadioGroupBox.Name = "DirectionRadioGroupBox";
            this.DirectionRadioGroupBox.Size = new System.Drawing.Size(373, 52);
            this.DirectionRadioGroupBox.TabIndex = 9;
            this.DirectionRadioGroupBox.TabStop = false;
            this.DirectionRadioGroupBox.Text = "Direction :";
            // 
            // WestRadioButton
            // 
            this.WestRadioButton.AutoSize = true;
            this.WestRadioButton.Location = new System.Drawing.Point(192, 25);
            this.WestRadioButton.Name = "WestRadioButton";
            this.WestRadioButton.Size = new System.Drawing.Size(55, 21);
            this.WestRadioButton.TabIndex = 3;
            this.WestRadioButton.Text = "West";
            this.WestRadioButton.UseVisualStyleBackColor = true;
            this.WestRadioButton.CheckedChanged += new System.EventHandler(this.WestRadioButton_CheckedChanged);
            // 
            // EastRadioButton
            // 
            this.EastRadioButton.AutoSize = true;
            this.EastRadioButton.Location = new System.Drawing.Point(137, 25);
            this.EastRadioButton.Name = "EastRadioButton";
            this.EastRadioButton.Size = new System.Drawing.Size(50, 21);
            this.EastRadioButton.TabIndex = 2;
            this.EastRadioButton.Text = "East";
            this.EastRadioButton.UseVisualStyleBackColor = true;
            this.EastRadioButton.CheckedChanged += new System.EventHandler(this.EastRadioButton_CheckedChanged);
            // 
            // SouthRadioButton
            // 
            this.SouthRadioButton.AutoSize = true;
            this.SouthRadioButton.Location = new System.Drawing.Point(72, 25);
            this.SouthRadioButton.Name = "SouthRadioButton";
            this.SouthRadioButton.Size = new System.Drawing.Size(59, 21);
            this.SouthRadioButton.TabIndex = 1;
            this.SouthRadioButton.Text = "South";
            this.SouthRadioButton.UseVisualStyleBackColor = true;
            this.SouthRadioButton.CheckedChanged += new System.EventHandler(this.SouthRadioButton_CheckedChanged);
            // 
            // NorthRadioButton
            // 
            this.NorthRadioButton.AutoSize = true;
            this.NorthRadioButton.Checked = true;
            this.NorthRadioButton.Location = new System.Drawing.Point(7, 26);
            this.NorthRadioButton.Name = "NorthRadioButton";
            this.NorthRadioButton.Size = new System.Drawing.Size(60, 21);
            this.NorthRadioButton.TabIndex = 0;
            this.NorthRadioButton.TabStop = true;
            this.NorthRadioButton.Text = "North";
            this.NorthRadioButton.UseVisualStyleBackColor = true;
            this.NorthRadioButton.CheckedChanged += new System.EventHandler(this.NorthRadioButton_CheckedChanged);
            // 
            // MaxTNTTextBox
            // 
            this.MaxTNTTextBox.Location = new System.Drawing.Point(4, 230);
            this.MaxTNTTextBox.Name = "MaxTNTTextBox";
            this.MaxTNTTextBox.Size = new System.Drawing.Size(366, 23);
            this.MaxTNTTextBox.TabIndex = 7;
            this.MaxTNTTextBox.TextChanged += new System.EventHandler(this.MaxTNTTextBox_TextChanged);
            // 
            // MaxTNTLabel
            // 
            this.MaxTNTLabel.AutoSize = true;
            this.MaxTNTLabel.Location = new System.Drawing.Point(4, 209);
            this.MaxTNTLabel.Name = "MaxTNTLabel";
            this.MaxTNTLabel.Size = new System.Drawing.Size(68, 17);
            this.MaxTNTLabel.TabIndex = 8;
            this.MaxTNTLabel.Text = "Max TNT :";
            // 
            // DestinationZTextBox
            // 
            this.DestinationZTextBox.Location = new System.Drawing.Point(4, 179);
            this.DestinationZTextBox.Name = "DestinationZTextBox";
            this.DestinationZTextBox.Size = new System.Drawing.Size(366, 23);
            this.DestinationZTextBox.TabIndex = 5;
            this.DestinationZTextBox.TextChanged += new System.EventHandler(this.DestinationZTextBox_TextChanged);
            // 
            // DestinationZLabel
            // 
            this.DestinationZLabel.AutoSize = true;
            this.DestinationZLabel.Location = new System.Drawing.Point(4, 158);
            this.DestinationZLabel.Name = "DestinationZLabel";
            this.DestinationZLabel.Size = new System.Drawing.Size(91, 17);
            this.DestinationZLabel.TabIndex = 6;
            this.DestinationZLabel.Text = "Destination Z :";
            // 
            // DestinationXTextBox
            // 
            this.DestinationXTextBox.Location = new System.Drawing.Point(4, 128);
            this.DestinationXTextBox.Name = "DestinationXTextBox";
            this.DestinationXTextBox.Size = new System.Drawing.Size(366, 23);
            this.DestinationXTextBox.TabIndex = 3;
            this.DestinationXTextBox.TextChanged += new System.EventHandler(this.DestinationXTextBox_TextChanged);
            // 
            // DestinationXLabel
            // 
            this.DestinationXLabel.AutoSize = true;
            this.DestinationXLabel.Location = new System.Drawing.Point(4, 107);
            this.DestinationXLabel.Name = "DestinationXLabel";
            this.DestinationXLabel.Size = new System.Drawing.Size(92, 17);
            this.DestinationXLabel.TabIndex = 4;
            this.DestinationXLabel.Text = "Destination X :";
            // 
            // PearlZTextBox
            // 
            this.PearlZTextBox.Location = new System.Drawing.Point(4, 77);
            this.PearlZTextBox.Name = "PearlZTextBox";
            this.PearlZTextBox.Size = new System.Drawing.Size(366, 23);
            this.PearlZTextBox.TabIndex = 1;
            this.PearlZTextBox.TextChanged += new System.EventHandler(this.PearlZTextBox_TextChanged);
            // 
            // PearlZLabel
            // 
            this.PearlZLabel.AutoSize = true;
            this.PearlZLabel.Location = new System.Drawing.Point(4, 56);
            this.PearlZLabel.Name = "PearlZLabel";
            this.PearlZLabel.Size = new System.Drawing.Size(55, 17);
            this.PearlZLabel.TabIndex = 2;
            this.PearlZLabel.Text = "Pearl Z :";
            // 
            // PearlXTextBox
            // 
            this.PearlXTextBox.Location = new System.Drawing.Point(4, 26);
            this.PearlXTextBox.Name = "PearlXTextBox";
            this.PearlXTextBox.Size = new System.Drawing.Size(366, 23);
            this.PearlXTextBox.TabIndex = 0;
            this.PearlXTextBox.TextChanged += new System.EventHandler(this.PearlXTextBox_TextChanged);
            // 
            // PearlXLabel
            // 
            this.PearlXLabel.AutoSize = true;
            this.PearlXLabel.Location = new System.Drawing.Point(4, 5);
            this.PearlXLabel.Name = "PearlXLabel";
            this.PearlXLabel.Size = new System.Drawing.Size(56, 17);
            this.PearlXLabel.TabIndex = 0;
            this.PearlXLabel.Text = "Pearl X :";
            // 
            // GeneralFTLAdvancedTabPage
            // 
            this.GeneralFTLAdvancedTabPage.Controls.Add(this.TNTWeightGroupBox);
            this.GeneralFTLAdvancedTabPage.Controls.Add(this.OffsetGroupBox);
            this.GeneralFTLAdvancedTabPage.Location = new System.Drawing.Point(4, 26);
            this.GeneralFTLAdvancedTabPage.Name = "GeneralFTLAdvancedTabPage";
            this.GeneralFTLAdvancedTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralFTLAdvancedTabPage.Size = new System.Drawing.Size(373, 557);
            this.GeneralFTLAdvancedTabPage.TabIndex = 1;
            this.GeneralFTLAdvancedTabPage.Text = "Advanced";
            this.GeneralFTLAdvancedTabPage.UseVisualStyleBackColor = true;
            // 
            // TNTWeightGroupBox
            // 
            this.TNTWeightGroupBox.Controls.Add(this.DistanceRadioButton);
            this.TNTWeightGroupBox.Controls.Add(this.TNTRadioButton);
            this.TNTWeightGroupBox.Controls.Add(this.DistanceWeightRadioButton);
            this.TNTWeightGroupBox.Controls.Add(this.TNTWeightTrackerSlider);
            this.TNTWeightGroupBox.Location = new System.Drawing.Point(4, 102);
            this.TNTWeightGroupBox.Name = "TNTWeightGroupBox";
            this.TNTWeightGroupBox.Size = new System.Drawing.Size(366, 117);
            this.TNTWeightGroupBox.TabIndex = 1;
            this.TNTWeightGroupBox.TabStop = false;
            this.TNTWeightGroupBox.Text = "TNT Weight :";
            this.TNTWeightGroupBox.Enter += new System.EventHandler(this.TNTWeightGroupBox_Enter);
            // 
            // DistanceRadioButton
            // 
            this.DistanceRadioButton.AutoSize = true;
            this.DistanceRadioButton.Location = new System.Drawing.Point(215, 84);
            this.DistanceRadioButton.Name = "DistanceRadioButton";
            this.DistanceRadioButton.Size = new System.Drawing.Size(105, 21);
            this.DistanceRadioButton.TabIndex = 3;
            this.DistanceRadioButton.TabStop = true;
            this.DistanceRadioButton.Text = "Only Distance";
            this.DistanceRadioButton.UseVisualStyleBackColor = true;
            // 
            // TNTRadioButton
            // 
            this.TNTRadioButton.AutoSize = true;
            this.TNTRadioButton.Location = new System.Drawing.Point(130, 84);
            this.TNTRadioButton.Name = "TNTRadioButton";
            this.TNTRadioButton.Size = new System.Drawing.Size(80, 21);
            this.TNTRadioButton.TabIndex = 2;
            this.TNTRadioButton.TabStop = true;
            this.TNTRadioButton.Text = "Only TNT";
            this.TNTRadioButton.UseVisualStyleBackColor = true;
            // 
            // DistanceWeightRadioButton
            // 
            this.DistanceWeightRadioButton.AutoSize = true;
            this.DistanceWeightRadioButton.Checked = true;
            this.DistanceWeightRadioButton.Location = new System.Drawing.Point(7, 85);
            this.DistanceWeightRadioButton.Name = "DistanceWeightRadioButton";
            this.DistanceWeightRadioButton.Size = new System.Drawing.Size(121, 21);
            this.DistanceWeightRadioButton.TabIndex = 1;
            this.DistanceWeightRadioButton.TabStop = true;
            this.DistanceWeightRadioButton.Text = "Distance Vs TNT";
            this.DistanceWeightRadioButton.UseVisualStyleBackColor = true;
            // 
            // TNTWeightTrackerSlider
            // 
            this.TNTWeightTrackerSlider.Location = new System.Drawing.Point(7, 26);
            this.TNTWeightTrackerSlider.Maximum = 100;
            this.TNTWeightTrackerSlider.Name = "TNTWeightTrackerSlider";
            this.TNTWeightTrackerSlider.Size = new System.Drawing.Size(353, 45);
            this.TNTWeightTrackerSlider.TabIndex = 0;
            this.TNTWeightTrackerSlider.Scroll += new System.EventHandler(this.TNTWeightTrackerSlider_Scroll);
            // 
            // OffsetGroupBox
            // 
            this.OffsetGroupBox.Controls.Add(this.OffsetZTextBox);
            this.OffsetGroupBox.Controls.Add(this.ZOffsetLabel);
            this.OffsetGroupBox.Controls.Add(this.OffsetXTextBox);
            this.OffsetGroupBox.Controls.Add(this.XOffsetLabel);
            this.OffsetGroupBox.Location = new System.Drawing.Point(4, 5);
            this.OffsetGroupBox.Name = "OffsetGroupBox";
            this.OffsetGroupBox.Size = new System.Drawing.Size(366, 90);
            this.OffsetGroupBox.TabIndex = 0;
            this.OffsetGroupBox.TabStop = false;
            this.OffsetGroupBox.Text = "Offsets :";
            // 
            // OffsetZTextBox
            // 
            this.OffsetZTextBox.Location = new System.Drawing.Point(34, 56);
            this.OffsetZTextBox.Name = "OffsetZTextBox";
            this.OffsetZTextBox.Size = new System.Drawing.Size(332, 23);
            this.OffsetZTextBox.TabIndex = 3;
            this.OffsetZTextBox.TextChanged += new System.EventHandler(this.OffsetZTextBox_TextChanged);
            // 
            // ZOffsetLabel
            // 
            this.ZOffsetLabel.AutoSize = true;
            this.ZOffsetLabel.Location = new System.Drawing.Point(7, 59);
            this.ZOffsetLabel.Name = "ZOffsetLabel";
            this.ZOffsetLabel.Size = new System.Drawing.Size(22, 17);
            this.ZOffsetLabel.TabIndex = 2;
            this.ZOffsetLabel.Text = "Z :";
            // 
            // OffsetXTextBox
            // 
            this.OffsetXTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.OffsetXTextBox.ImeMode = System.Windows.Forms.ImeMode.On;
            this.OffsetXTextBox.Location = new System.Drawing.Point(34, 23);
            this.OffsetXTextBox.Name = "OffsetXTextBox";
            this.OffsetXTextBox.Size = new System.Drawing.Size(332, 23);
            this.OffsetXTextBox.TabIndex = 1;
            this.OffsetXTextBox.TextChanged += new System.EventHandler(this.OffsetXTextBox_TextChanged);
            // 
            // XOffsetLabel
            // 
            this.XOffsetLabel.AutoSize = true;
            this.XOffsetLabel.Location = new System.Drawing.Point(7, 26);
            this.XOffsetLabel.Name = "XOffsetLabel";
            this.XOffsetLabel.Size = new System.Drawing.Size(23, 17);
            this.XOffsetLabel.TabIndex = 0;
            this.XOffsetLabel.Text = "X :";
            // 
            // GeneralFTLSettingsTabPage
            // 
            this.GeneralFTLSettingsTabPage.Controls.Add(this.SettingInputTextBox);
            this.GeneralFTLSettingsTabPage.Controls.Add(this.ChangeSettingButton);
            this.GeneralFTLSettingsTabPage.Controls.Add(this.ResetSettingButton);
            this.GeneralFTLSettingsTabPage.Controls.Add(this.SettingListView);
            this.GeneralFTLSettingsTabPage.Location = new System.Drawing.Point(4, 26);
            this.GeneralFTLSettingsTabPage.Name = "GeneralFTLSettingsTabPage";
            this.GeneralFTLSettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralFTLSettingsTabPage.Size = new System.Drawing.Size(373, 557);
            this.GeneralFTLSettingsTabPage.TabIndex = 2;
            this.GeneralFTLSettingsTabPage.Text = "Settings";
            this.GeneralFTLSettingsTabPage.UseVisualStyleBackColor = true;
            // 
            // SettingInputTextBox
            // 
            this.SettingInputTextBox.Location = new System.Drawing.Point(4, 458);
            this.SettingInputTextBox.Name = "SettingInputTextBox";
            this.SettingInputTextBox.Size = new System.Drawing.Size(366, 23);
            this.SettingInputTextBox.TabIndex = 3;
            this.SettingInputTextBox.TextChanged += new System.EventHandler(this.SettingInputTextBox_TextChanged);
            // 
            // ChangeSettingButton
            // 
            this.ChangeSettingButton.Location = new System.Drawing.Point(4, 491);
            this.ChangeSettingButton.Name = "ChangeSettingButton";
            this.ChangeSettingButton.Size = new System.Drawing.Size(366, 26);
            this.ChangeSettingButton.TabIndex = 2;
            this.ChangeSettingButton.Text = "Change Setting";
            this.ChangeSettingButton.UseVisualStyleBackColor = true;
            // 
            // ResetSettingButton
            // 
            this.ResetSettingButton.Location = new System.Drawing.Point(4, 524);
            this.ResetSettingButton.Name = "ResetSettingButton";
            this.ResetSettingButton.Size = new System.Drawing.Size(366, 26);
            this.ResetSettingButton.TabIndex = 1;
            this.ResetSettingButton.Text = "Reset To Default";
            this.ResetSettingButton.UseVisualStyleBackColor = true;
            // 
            // SettingListView
            // 
            this.SettingListView.FullRowSelect = true;
            this.SettingListView.HideSelection = false;
            this.SettingListView.Location = new System.Drawing.Point(4, 5);
            this.SettingListView.Name = "SettingListView";
            this.SettingListView.Size = new System.Drawing.Size(366, 446);
            this.SettingListView.TabIndex = 0;
            this.SettingListView.UseCompatibleStateImageBehavior = false;
            this.SettingListView.View = System.Windows.Forms.View.Details;
            // 
            // InputTabControl
            // 
            this.InputTabControl.Controls.Add(this.GeneralFTLInputTabPage);
            this.InputTabControl.Location = new System.Drawing.Point(13, 15);
            this.InputTabControl.Name = "InputTabControl";
            this.InputTabControl.SelectedIndex = 0;
            this.InputTabControl.Size = new System.Drawing.Size(389, 621);
            this.InputTabControl.TabIndex = 0;
            // 
            // BasicInputOutputSystem
            // 
            this.BasicInputOutputSystem.Controls.Add(this.OutputTabPage);
            this.BasicInputOutputSystem.Controls.Add(this.InputTabPage);
            this.BasicInputOutputSystem.Location = new System.Drawing.Point(409, 15);
            this.BasicInputOutputSystem.Name = "BasicInputOutputSystem";
            this.BasicInputOutputSystem.SelectedIndex = 0;
            this.BasicInputOutputSystem.Size = new System.Drawing.Size(379, 617);
            this.BasicInputOutputSystem.TabIndex = 1;
            // 
            // OutputTabPage
            // 
            this.OutputTabPage.Controls.Add(this.BasicDirectionOutSystem);
            this.OutputTabPage.Controls.Add(this.BasicOutputSystem);
            this.OutputTabPage.Location = new System.Drawing.Point(4, 26);
            this.OutputTabPage.Name = "OutputTabPage";
            this.OutputTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.OutputTabPage.Size = new System.Drawing.Size(371, 587);
            this.OutputTabPage.TabIndex = 0;
            this.OutputTabPage.Text = "Display";
            this.OutputTabPage.UseVisualStyleBackColor = true;
            // 
            // BasicDirectionOutSystem
            // 
            this.BasicDirectionOutSystem.FullRowSelect = true;
            this.BasicDirectionOutSystem.HideSelection = false;
            this.BasicDirectionOutSystem.Location = new System.Drawing.Point(4, 524);
            this.BasicDirectionOutSystem.Name = "BasicDirectionOutSystem";
            this.BasicDirectionOutSystem.Size = new System.Drawing.Size(364, 58);
            this.BasicDirectionOutSystem.TabIndex = 1;
            this.BasicDirectionOutSystem.UseCompatibleStateImageBehavior = false;
            this.BasicDirectionOutSystem.View = System.Windows.Forms.View.Details;
            // 
            // BasicOutputSystem
            // 
            this.BasicOutputSystem.FullRowSelect = true;
            this.BasicOutputSystem.HideSelection = false;
            this.BasicOutputSystem.Location = new System.Drawing.Point(4, 5);
            this.BasicOutputSystem.Name = "BasicOutputSystem";
            this.BasicOutputSystem.Size = new System.Drawing.Size(364, 512);
            this.BasicOutputSystem.TabIndex = 0;
            this.BasicOutputSystem.UseCompatibleStateImageBehavior = false;
            this.BasicOutputSystem.View = System.Windows.Forms.View.Details;
            this.BasicOutputSystem.SelectedIndexChanged += new System.EventHandler(this.BasicOutputSystem_SelectedIndexChanged);
            // 
            // InputTabPage
            // 
            this.InputTabPage.Controls.Add(this.ConsoleInputTextBox);
            this.InputTabPage.Controls.Add(this.ConsoleEnterButton);
            this.InputTabPage.Controls.Add(this.ConsoleListView);
            this.InputTabPage.Location = new System.Drawing.Point(4, 26);
            this.InputTabPage.Name = "InputTabPage";
            this.InputTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.InputTabPage.Size = new System.Drawing.Size(371, 587);
            this.InputTabPage.TabIndex = 1;
            this.InputTabPage.Text = "Console";
            this.InputTabPage.UseVisualStyleBackColor = true;
            // 
            // ConsoleInputTextBox
            // 
            this.ConsoleInputTextBox.Location = new System.Drawing.Point(4, 522);
            this.ConsoleInputTextBox.Name = "ConsoleInputTextBox";
            this.ConsoleInputTextBox.Size = new System.Drawing.Size(364, 23);
            this.ConsoleInputTextBox.TabIndex = 2;
            // 
            // ConsoleEnterButton
            // 
            this.ConsoleEnterButton.Location = new System.Drawing.Point(4, 555);
            this.ConsoleEnterButton.Name = "ConsoleEnterButton";
            this.ConsoleEnterButton.Size = new System.Drawing.Size(364, 26);
            this.ConsoleEnterButton.TabIndex = 1;
            this.ConsoleEnterButton.Text = "Enter";
            this.ConsoleEnterButton.UseVisualStyleBackColor = true;
            // 
            // ConsoleListView
            // 
            this.ConsoleListView.HideSelection = false;
            this.ConsoleListView.Location = new System.Drawing.Point(4, 5);
            this.ConsoleListView.Name = "ConsoleListView";
            this.ConsoleListView.Size = new System.Drawing.Size(364, 512);
            this.ConsoleListView.TabIndex = 0;
            this.ConsoleListView.UseCompatibleStateImageBehavior = false;
            this.ConsoleListView.View = System.Windows.Forms.View.Details;
            // 
            // PearlCalculatorWFA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 647);
            this.Controls.Add(this.BasicInputOutputSystem);
            this.Controls.Add(this.InputTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "PearlCalculatorWFA";
            this.Text = "Pearl Calculator V2.71";
            this.GeneralFTLInputTabPage.ResumeLayout(false);
            this.GeneralFTLTabControl.ResumeLayout(false);
            this.GeneralFTLGeneralTabPage.ResumeLayout(false);
            this.GeneralFTLGeneralTabPage.PerformLayout();
            this.DirectionRadioGroupBox.ResumeLayout(false);
            this.DirectionRadioGroupBox.PerformLayout();
            this.GeneralFTLAdvancedTabPage.ResumeLayout(false);
            this.TNTWeightGroupBox.ResumeLayout(false);
            this.TNTWeightGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TNTWeightTrackerSlider)).EndInit();
            this.OffsetGroupBox.ResumeLayout(false);
            this.OffsetGroupBox.PerformLayout();
            this.GeneralFTLSettingsTabPage.ResumeLayout(false);
            this.GeneralFTLSettingsTabPage.PerformLayout();
            this.InputTabControl.ResumeLayout(false);
            this.BasicInputOutputSystem.ResumeLayout(false);
            this.OutputTabPage.ResumeLayout(false);
            this.InputTabPage.ResumeLayout(false);
            this.InputTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage GeneralFTLInputTabPage;
        private System.Windows.Forms.TabControl InputTabControl;
        private System.Windows.Forms.TabControl GeneralFTLTabControl;
        private System.Windows.Forms.TabPage GeneralFTLGeneralTabPage;
        private System.Windows.Forms.TabPage GeneralFTLAdvancedTabPage;
        private System.Windows.Forms.TabPage GeneralFTLSettingsTabPage;
        private System.Windows.Forms.TextBox MaxTNTTextBox;
        private System.Windows.Forms.Label MaxTNTLabel;
        private System.Windows.Forms.TextBox DestinationZTextBox;
        private System.Windows.Forms.Label DestinationZLabel;
        private System.Windows.Forms.TextBox DestinationXTextBox;
        private System.Windows.Forms.Label DestinationXLabel;
        private System.Windows.Forms.TextBox PearlZTextBox;
        private System.Windows.Forms.Label PearlZLabel;
        private System.Windows.Forms.TextBox PearlXTextBox;
        private System.Windows.Forms.Label PearlXLabel;
        private System.Windows.Forms.GroupBox DirectionRadioGroupBox;
        private System.Windows.Forms.RadioButton WestRadioButton;
        private System.Windows.Forms.RadioButton EastRadioButton;
        private System.Windows.Forms.RadioButton SouthRadioButton;
        private System.Windows.Forms.RadioButton NorthRadioButton;
        private System.Windows.Forms.TextBox BlueTNTTextBox;
        private System.Windows.Forms.Label BlueTNTLabel;
        private System.Windows.Forms.TextBox RedTNTTextBox;
        private System.Windows.Forms.Label RedTNTLabel;
        private System.Windows.Forms.Button SaveSettingButton;
        private System.Windows.Forms.Button ImportSettingButton;
        private System.Windows.Forms.Button PearlSimulateButton;
        private System.Windows.Forms.Button CalculateTNTButton;
        private System.Windows.Forms.TabControl BasicInputOutputSystem;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage InputTabPage;
        private System.Windows.Forms.TabPage OutputTabPage;
        private System.Windows.Forms.ListView BasicDirectionOutSystem;
        private System.Windows.Forms.ListView BasicOutputSystem;
        private System.Windows.Forms.TextBox SettingInputTextBox;
        private System.Windows.Forms.Button ChangeSettingButton;
        private System.Windows.Forms.Button ResetSettingButton;
        private System.Windows.Forms.ListView SettingListView;
        private System.Windows.Forms.TextBox ConsoleInputTextBox;
        private System.Windows.Forms.Button ConsoleEnterButton;
        private System.Windows.Forms.ListView ConsoleListView;
        private System.Windows.Forms.GroupBox OffsetGroupBox;
        private System.Windows.Forms.TextBox OffsetZTextBox;
        private System.Windows.Forms.Label ZOffsetLabel;
        private System.Windows.Forms.TextBox OffsetXTextBox;
        private System.Windows.Forms.Label XOffsetLabel;
        private System.Windows.Forms.GroupBox TNTWeightGroupBox;
        private System.Windows.Forms.TrackBar TNTWeightTrackerSlider;
        private System.Windows.Forms.RadioButton DistanceRadioButton;
        private System.Windows.Forms.RadioButton TNTRadioButton;
        private System.Windows.Forms.RadioButton DistanceWeightRadioButton;
    }
}

