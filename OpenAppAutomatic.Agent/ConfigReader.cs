namespace OpenAppAutomatic;

public class ConfigReader
{
    private string _configRoot;
    
    public ConfigReader(string configRoot)
    {
        _configRoot = configRoot;
    }

    public List<ProcessConfig> Read()
    {
        var configs = new List<ProcessConfig>();
        var configLines = File.ReadAllLines(_configRoot);
        foreach (var line in configLines.Where(l => !string.IsNullOrEmpty(l)))
        {
            configs.Add(GetDataFromCsv(line));
        }

        return configs;
    }
    
    private ProcessConfig GetDataFromCsv(string csvLine)
    {
        var columns = csvLine.Split(',');
        var title = columns[0];
        var processName = columns[1];
        var execPath = columns[2];
        var argument = columns[3];
        var timeString = columns[4];
        var scheduleTime = TimeSpan.Parse(timeString);

        return new ProcessConfig(title, execPath, argument, processName, scheduleTime);
    }
}