using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using NeoSmart.SecureStore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharifLoginWindowsApplication
{
    public partial class SharifLoginServiceForm : Form
    {
        private readonly ILogger<SharifLoginServiceForm> _logger;
        private readonly MySecretsManager _mySecretsManager;
        private readonly StartupManager _startupManager;
        private readonly LoginManager _loginManager;

        public SharifLoginServiceForm(ILogger<SharifLoginServiceForm> logger, MySecretsManager mySecretsManager, StartupManager startupManager, LoginManager loginManager)
        {
            _logger = logger;
            _mySecretsManager = mySecretsManager;
            _startupManager = startupManager;
            _loginManager = loginManager;
            InitializeComponent();
        }

        private void SharifLoginServiceForm_Load(object sender, EventArgs e)
        {
            // Retrieving secrets
            if(_mySecretsManager.TryRetrieve(out LoginCredentials loginCredentials))
            {
                usernameTextBox.Text = loginCredentials.Username;
                passwordTextBox.Text = loginCredentials.Password;
            }

            // Retrieving startup 
            runOnStartupCheckBox.Checked = _startupManager.IsRunOnStartUp();

            // Retrieving service status
            if(runOnStartupCheckBox.Checked)
            {
                loginBackgroundWorker.RunWorkerAsync();
                StartServiceConfig();
            }
            else
                StopServiceConfig();

            intervalNumericUpDown.Value = Settings.Default.IntervalDuration;
        }

        private void runOnStartupCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (runOnStartupCheckBox.Checked)
                _startupManager.SetRunOnStartup();
            else
                _startupManager.UnSetRunOnStartup();
        }

        private async void startServiceButton_Click(object sender, EventArgs e)
        {
            var username = usernameTextBox.Text;
            var password = passwordTextBox.Text;

            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please provide username and password correctly!", "Fields Empty!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            _mySecretsManager.Store(new LoginCredentials { Username = username, Password = password });
            Settings.Default.IntervalDuration = (int)intervalNumericUpDown.Value;
            Settings.Default.Save();

            BeginStartServiceConfig();
            if (loginBackgroundWorker.IsBusy)
                loginBackgroundWorker.CancelAsync();
            while (loginBackgroundWorker.CancellationPending) await Task.Delay(10);
            
            loginBackgroundWorker.RunWorkerAsync();

            EndStartServiceConfig();
        }

        private async void stopServiceButton_Click(object sender, EventArgs e)
        {
            BeginStopServiceConfig();
            if (loginBackgroundWorker.IsBusy)
                loginBackgroundWorker.CancelAsync();
            while (loginBackgroundWorker.CancellationPending) await Task.Delay(10);
            
            EndStopServiceConfig();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Set the WindowState to normal if the form is minimized.
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;

            // Activate the form.
            this.Activate();
            this.Visible = true;
        }

        private void SharifLoginServiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(e.CloseReason != CloseReason.ApplicationExitCall)
                e.Cancel = true;
            
            this.Visible = false;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BeginStartServiceConfig()
        {
            statusTextBox.Text = "Starting the service...";
            startServiceButton.Enabled = false;
            startToolStripMenuItem.Enabled = false;
        }

        private void EndStartServiceConfig()
        {
            stopServiceButton.Enabled = true;
            stopToolStripMenuItem.Enabled = true;

            notifyIcon.Icon = Resources.logo_green_Icon;
            statusTextBox.Text = "Service started!";
        }

        private void StartServiceConfig()
        {
            BeginStartServiceConfig();
            EndStartServiceConfig();
        }

        private void BeginStopServiceConfig()
        {
            statusTextBox.Text = "Stopping the service...";
            stopServiceButton.Enabled = false;
            stopToolStripMenuItem.Enabled = false;
        }

        private void EndStopServiceConfig()
        {
            startServiceButton.Enabled = true;
            startToolStripMenuItem.Enabled = true;

            notifyIcon.Icon = Resources.logo_red_Icon;
            statusTextBox.Text = "Service Stopped!";
        }

        private void StopServiceConfig()
        {
            BeginStopServiceConfig();
            EndStopServiceConfig();
        }

        private void loginBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            _loginManager.ExecuteAsync(worker, Settings.Default.IntervalDuration).Wait();
        }

        private void loginBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message, "Error on running login service", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loginBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            statusTextBox.Text = e?.UserState?.ToString() ?? String.Empty;
        }
    }
}
