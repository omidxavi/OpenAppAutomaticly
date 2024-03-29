namespace OpenAppAutomatic;

public class ProcessConfiguration
{
    public string Title { get; }
    public string ExePath { get; }
    public string Argument { get; }
    public string ProcessName { get; }
    public TimeSpan ScheduleTime { get; }
    public bool IsRunning = false;
    
    public ProcessConfiguration(string title, string exePath, string argument, string processName, TimeSpan scheduleTime)
    {
        Title = title;
        ExePath = exePath;
        Argument = argument;
        ProcessName = processName;
        ScheduleTime = scheduleTime;
    }
}