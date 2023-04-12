using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharifLoginWindowsApplication
{
    public class StartupManager
    {
        private readonly string _startupKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        private readonly string _startupValue = "SharifLoginWindowsApplicaiton";
        public  bool IsRunOnStartUp()
        {
            RegistryKey key = Registry.CurrentUser?.OpenSubKey(_startupKey, false);
            return key?.GetValue(_startupValue) != null;
        }

        public void SetRunOnStartup()
        {
            RegistryKey key = Registry.CurrentUser?.OpenSubKey(_startupKey, true);
            key?.SetValue(_startupValue, Application.ExecutablePath.ToString());
        }

        public void UnSetRunOnStartup()
        {
            RegistryKey key = Registry.CurrentUser?.OpenSubKey(_startupKey, true);
            key?.DeleteValue(_startupValue, false);
        }
    }
}
