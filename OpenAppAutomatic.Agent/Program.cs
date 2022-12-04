// See https://aka.ms/new-console-template for more information


using Microsoft.Extensions.Configuration;
using Serilog;

namespace OpenAppAutomatic
{
    class Program
    {
        private static List<ProcessConfiguration> _processes;
        private static ConfigReader _configReader;
        private static string _configPath = Path.Combine(Directory.GetCurrentDirectory(), "Config.csv");
        private static TimeValidator _timeValidator;
        private static ProcessHandler _processHandler;

        static void Main(string[] args)
        {
            ConfigApplication();
            _configReader = new ConfigReader(_configPath);
            _timeValidator = new TimeValidator();
            _processHandler = new ProcessHandler();
            _processes = _configReader.Read();

            while (true)
            {
                try
                {
                    var validatedProcesses = _timeValidator.validate(_processes);

                    foreach (var process in validatedProcesses)
                    {
                        _processHandler.KillProcess(process);
                    }

                    Thread.Sleep(5000);

                    foreach (var process in validatedProcesses)
                    {
                        _processHandler.RunNewProcess(process);
                    }
                }
                catch (Exception e)
                {
                    Log.Information("{Error :}", e.Message);
                }
            }
        }

        private static void ConfigApplication()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                    true)
                .Build();

            ConfigLogging(configuration);
        }

        private static void ConfigLogging(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}