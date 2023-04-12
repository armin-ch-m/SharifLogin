using NeoSmart.SecureStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharifLoginWindowsApplication
{
    public class MySecretsManager
    {
        private readonly string _appDataPath;
        private readonly string _keyPath;
        private readonly string _storePath;


        public MySecretsManager()
        {
            _appDataPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SharifLogin");
            _keyPath = Path.Join(_appDataPath, "secrets.key");
            _storePath = Path.Join(_appDataPath, "secrets.bin");
        }
        
        public void Store(LoginCredentials loginCredentials)
        {
            if (!Directory.Exists(_appDataPath))
                Directory.CreateDirectory(_appDataPath);
            using (var sman = SecretsManager.CreateStore())
            {
                if (File.Exists(_keyPath))
                {
                    // Use an existing key file:
                    sman.LoadKeyFromFile(_keyPath);
                }
                else
                {
                    // Create a new key securely with a CSPRNG:
                    sman.GenerateKey();
                }

                sman.Set("username", loginCredentials.Username);
                sman.Set("password", loginCredentials.Password);

                // Export the keyfile (even if you created the store with a password)
                sman.ExportKey(_keyPath);

                // Save the store 
                sman.SaveStore(_storePath);
            }
        }

        public bool TryRetrieve(out LoginCredentials loginCredentials)
        {
            loginCredentials = null;

            if (File.Exists(_storePath) && File.Exists(_keyPath))
            {
                using (var sman = SecretsManager.LoadStore(_storePath))
                {
                    sman.LoadKeyFromFile(_keyPath);

                    loginCredentials = new LoginCredentials
                    {
                        Username = sman.Get("username"),
                        Password = sman.Get("password")
                    };
                    
                    return true;
                }
            }

            return false;
        }
    }
}
