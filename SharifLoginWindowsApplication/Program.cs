using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SharifLoginWindowsApplication
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool result;
            var mutex = new System.Threading.Mutex(true, "SharifLoginWindowsApp", out result);

            if (!result)
            {
                MessageBox.Show("Another instance is already running.");
                return;
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            Application.Run(ServiceProvider.GetRequiredService<SharifLoginServiceForm>());

            GC.KeepAlive(mutex);                // mutex shouldn't be released
        }

        public static IServiceProvider ServiceProvider { get; private set; }
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddTransient<MySecretsManager>();
                    services.AddTransient<StartupManager>();
                    services.AddTransient<LoginManager>();
                    services.AddTransient<HttpClient>();
                    services.AddTransient<SharifLoginServiceForm>();
                });
        }
    }
}