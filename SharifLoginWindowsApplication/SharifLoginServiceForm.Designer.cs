namespace SharifLoginWindowsApplication
{
    partial class SharifLoginServiceForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SharifLoginServiceForm));
            this.ussernameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.startServiceButton = new System.Windows.Forms.Button();
            this.stopServiceButton = new System.Windows.Forms.Button();
            this.runOnStartupCheckBox = new System.Windows.Forms.CheckBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.statusTextBox = new System.Windows.Forms.TextBox();
            this.intervalLabel = new System.Windows.Forms.Label();
            this.intervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intervalNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // ussernameLabel
            // 
            this.ussernameLabel.AutoSize = true;
            this.ussernameLabel.Location = new System.Drawing.Point(33, 42);
            this.ussernameLabel.Name = "ussernameLabel";
            this.ussernameLabel.Size = new System.Drawing.Size(75, 20);
            this.ussernameLabel.TabIndex = 0;
            this.ussernameLabel.Text = "Username";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(33, 95);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(70, 20);
            this.passwordLabel.TabIndex = 1;
            this.passwordLabel.Text = "Password";
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(149, 39);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(240, 27);
            this.usernameTextBox.TabIndex = 2;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(149, 92);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(240, 27);
            this.passwordTextBox.TabIndex = 3;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // startServiceButton
            // 
            this.startServiceButton.Location = new System.Drawing.Point(189, 205);
            this.startServiceButton.Name = "startServiceButton";
            this.startServiceButton.Size = new System.Drawing.Size(173, 29);
            this.startServiceButton.TabIndex = 4;
            this.startServiceButton.Text = "Start/Restart Service";
            this.startServiceButton.UseVisualStyleBackColor = true;
            this.startServiceButton.Click += new System.EventHandler(this.startServiceButton_Click);
            // 
            // stopServiceButton
            // 
            this.stopServiceButton.Location = new System.Drawing.Point(189, 263);
            this.stopServiceButton.Name = "stopServiceButton";
            this.stopServiceButton.Size = new System.Drawing.Size(173, 29);
            this.stopServiceButton.TabIndex = 5;
            this.stopServiceButton.Text = "Stop Service";
            this.stopServiceButton.UseVisualStyleBackColor = true;
            this.stopServiceButton.Click += new System.EventHandler(this.stopServiceButton_Click);
            // 
            // runOnStartupCheckBox
            // 
            this.runOnStartupCheckBox.AutoSize = true;
            this.runOnStartupCheckBox.Location = new System.Drawing.Point(426, 42);
            this.runOnStartupCheckBox.Name = "runOnStartupCheckBox";
            this.runOnStartupCheckBox.Size = new System.Drawing.Size(131, 24);
            this.runOnStartupCheckBox.TabIndex = 6;
            this.runOnStartupCheckBox.Text = "Run On Startup";
            this.runOnStartupCheckBox.UseVisualStyleBackColor = true;
            this.runOnStartupCheckBox.CheckedChanged += new System.EventHandler(this.runOnStartupCheckBox_CheckedChanged);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Sharif Login";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(162, 76);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(161, 24);
            this.startToolStripMenuItem.Text = "Start/Restart";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startServiceButton_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(161, 24);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopServiceButton_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(161, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // loginBackgroundWorker
            // 
            this.loginBackgroundWorker.WorkerReportsProgress = true;
            this.loginBackgroundWorker.WorkerSupportsCancellation = true;
            this.loginBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.loginBackgroundWorker_DoWork);
            this.loginBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.loginBackgroundWorker_ProgressChanged);
            this.loginBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.loginBackgroundWorker_RunWorkerCompleted);
            // 
            // statusTextBox
            // 
            this.statusTextBox.Location = new System.Drawing.Point(405, 95);
            this.statusTextBox.Multiline = true;
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.ReadOnly = true;
            this.statusTextBox.Size = new System.Drawing.Size(165, 197);
            this.statusTextBox.TabIndex = 8;
            this.statusTextBox.Text = "Waiting for status...";
            // 
            // intervalLabel
            // 
            this.intervalLabel.AutoSize = true;
            this.intervalLabel.Location = new System.Drawing.Point(12, 144);
            this.intervalLabel.Name = "intervalLabel";
            this.intervalLabel.Size = new System.Drawing.Size(120, 20);
            this.intervalLabel.TabIndex = 9;
            this.intervalLabel.Text = "Interval Duration";
            // 
            // intervalNumericUpDown
            // 
            this.intervalNumericUpDown.Location = new System.Drawing.Point(149, 145);
            this.intervalNumericUpDown.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.intervalNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.intervalNumericUpDown.Name = "intervalNumericUpDown";
            this.intervalNumericUpDown.Size = new System.Drawing.Size(240, 27);
            this.intervalNumericUpDown.TabIndex = 10;
            this.intervalNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SharifLoginServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 315);
            this.Controls.Add(this.intervalNumericUpDown);
            this.Controls.Add(this.intervalLabel);
            this.Controls.Add(this.statusTextBox);
            this.Controls.Add(this.runOnStartupCheckBox);
            this.Controls.Add(this.stopServiceButton);
            this.Controls.Add(this.startServiceButton);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.ussernameLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SharifLoginServiceForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sharif Login";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SharifLoginServiceForm_FormClosing);
            this.Load += new System.EventHandler(this.SharifLoginServiceForm_Load);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.intervalNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label ussernameLabel;
        private Label passwordLabel;
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button startServiceButton;
        private Button stopServiceButton;
        private CheckBox runOnStartupCheckBox;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem exitToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker loginBackgroundWorker;
        private ToolStripMenuItem startToolStripMenuItem;
        private ToolStripMenuItem stopToolStripMenuItem;
        private TextBox statusTextBox;
        private Label intervalLabel;
        private NumericUpDown intervalNumericUpDown;
    }
}