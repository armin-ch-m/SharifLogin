using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SharifLoginWindowsApplication
{
    public class LoginManager
    {

        private readonly ILogger<LoginManager> _logger;
        private readonly HttpClient _httpClient;
        private readonly MySecretsManager _mySecretsManager;
        private bool _networkAvailabillity;

        public LoginManager(ILogger<LoginManager> logger, HttpClient httpClient, MySecretsManager mySecretsManager)
        {
            _logger = logger;
            _httpClient = httpClient;
            _mySecretsManager = mySecretsManager;
            _networkAvailabillity = NetworkInterface.GetIsNetworkAvailable();
        }

        public async Task ExecuteAsync(BackgroundWorker worker, int intervalDuration)
        {
            // Create CancellationToken
            CancellationTokenSource cts = new CancellationTokenSource();
            var cancellationTask = Task.Run(() => {
                while (!worker.CancellationPending)
                    Task.Delay(100);
                cts.Cancel();
            });
            var cancellationToken = cts.Token;

            var status = string.Empty;

            NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
            _mySecretsManager.TryRetrieve(out LoginCredentials loginCredentials);

            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    if (_networkAvailabillity)
                    {
                        var isConnected = await CheckInternetConnection(cancellationToken);
                        if (!isConnected)
                        {
                            worker.ReportProgress(0, "No Connection!");
                            _logger.LogInformation("There is no internet connection.");
                            var isLoggedIn = await LoginToSharifAsync(loginCredentials, cancellationToken);
                            if (isLoggedIn)
                            {
                                status = await GetStatusAsync(cancellationToken);
                                worker.ReportProgress(0, status);
                            }
                        }
                    }
                    if (string.IsNullOrWhiteSpace(status))
                    {
                        status = await GetStatusAsync(cancellationToken);
                        worker.ReportProgress(0, status);
                    }

                    await Task.Delay(intervalDuration * 1000, cancellationToken);
                }
            }
            catch (TaskCanceledException) { }
            

            await cancellationTask;
        }

        private void NetworkChange_NetworkAvailabilityChanged(object? sender, NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable)
                _logger.LogInformation("Network is available");
            else
                _logger.LogInformation("Network is not available");

            _networkAvailabillity = e.IsAvailable;
        }

        private async Task<bool> CheckInternetConnection(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Trying to ping 8.8.8.8");
            try
            {
                using var ping = new Ping();
                var reply = await ping.SendPingAsync("8.8.8.8", 10000);

                if (reply.Status != IPStatus.Success)
                    throw new Exception($"The ping reply is {reply.Status}");

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to ping the 8.8.8.8. Exception Message: {e.Message}");
                return false;
            }
        }

        private async Task<bool> LoginToSharifAsync(LoginCredentials loginCredentials, CancellationToken cancellationToken)
        {
            var payload = $"username={loginCredentials.Username}&password={loginCredentials.Password}";
            var data = new StringContent(payload, Encoding.UTF8, "application/x-www-form-urlencoded");
            try
            {
                _logger.LogInformation("Trying to login 'https://net2.sharif.edu/login'");

                var response = await _httpClient.PostAsync("https://net2.sharif.edu/login", data, cancellationToken);
                var content = await response.Content.ReadAsStringAsync(cancellationToken);

                if (response.IsSuccessStatusCode && Regex.IsMatch(content, @"net2\.sharif\.edu/status"))
                {
                    _logger.LogInformation("Successful login");
                    return true;
                }
            }
            catch {}

            _logger.LogError("Failed to login 'https://net2.sharif.edu/login'");
            try
            {
                _logger.LogInformation("Trying to login 'https://172.17.1.214/login'");
                var response = await _httpClient.PostAsync("https://172.17.1.214/login", data, cancellationToken);
                var content = await response.Content.ReadAsStringAsync(cancellationToken);

                if (response.IsSuccessStatusCode && Regex.IsMatch(content, @"net2\.sharif\.edu/status"))
                {
                    _logger.LogInformation("Successful login");
                    return true;
                }
            }
            catch{}

            _logger.LogError("Failed to login 'https://172.17.1.214/login'");

            return false;
        }

        private async Task<string> GetStatusAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving Status from 'http://net2.sharif.edu/status'...");

            try
            {
                var response = await _httpClient.GetAsync("http://net2.sharif.edu/status", cancellationToken);
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var regex = new Regex("<td>(.*)</td>");
                    var status = string.Join(Environment.NewLine, regex.Matches(content).Select(m => m.Groups[1].Value.Trim()));
                    _logger.LogInformation($"Status:\n{status}");

                    return status;
                }
            }
            catch { }

            _logger.LogInformation("Retrieving Status from 'http://172.17.1.214/status'...");
            try
            {
                var response = await _httpClient.GetAsync("http://172.17.1.214/status", cancellationToken);
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var regex = new Regex("<td>(.*)</td>");
                    var status = string.Join(Environment.NewLine, regex.Matches(content).Select(m => m.Groups[1].Value.Trim()));
                    _logger.LogInformation($"Status:\n{status}");
                    return status;
                }
            }
            catch { }

            return "No Status!";
        }
    }
}
