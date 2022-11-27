// See https://aka.ms/new-console-template for more information


namespace OpenAppAutomatic
{
    class Program
    {

        private static List<ProcessConfig> _processes;
        private static ConfigReader _configReader;
        private static string _configPath = Path.Combine(Directory.GetCurrentDirectory(), "Config.csv");
        private static TimeValidator _timeValidator;
        private static ProcessHandler _processHandler;

        static void Main(string[] args)
        {
            
            _configReader = new ConfigReader(_configPath);
            _timeValidator = new TimeValidator();
            _processHandler = new ProcessHandler();
            _processes = _configReader.Read();
            
            while (true)
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
        }
    }
}