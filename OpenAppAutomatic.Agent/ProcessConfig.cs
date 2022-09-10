using System.Diagnostics;

namespace OpenAppAutomatic;

public class ProcessConfig
{
    public string Title { get; }
    private string ExePath { get; }
    private string Argument { get; }
    private string ProcessName { get; }
    private TimeSpan ScheduleTime { get; }

    public ProcessConfig(string line)
    {
        var columns = line.Split(',');
        Title = columns[0];
        ProcessName = columns[1];
        var path = columns[2];
        

        var array = path.Split(' ');
        if (array.Length > 1)
        {
            ExePath = array[0];
            Argument = array[1];
        }
        else
        {
            ExePath = path;
        }
        
        var timeString = columns[3];
        ScheduleTime = TimeSpan.Parse(timeString);
    }

    public bool IsInTime()
    {
        var now = DateTime.Now.TimeOfDay;
        now = new TimeSpan(now.Hours, now.Minutes, 0);
        if (now == ScheduleTime)
        {
            return true;
        }
        else
        {
            Console.WriteLine(now);
            return false;
        }
    }

    private void RunNewProcess(string exePath)
    {
        var startInfo = new ProcessStartInfo(exePath)
        {
            UseShellExecute = true
        };
        Process.Start(startInfo);
    }

    private void RunNewProcess(string exePath, string argument)
    {
        var startInfo = new ProcessStartInfo(exePath, argument)
        {
            UseShellExecute = true
        };
        Process.Start(startInfo);
    }

    private void KillProcess()
    {
        foreach (var process in Process.GetProcessesByName(ProcessName))
        {
            process.Kill();
        }
    }

    public void Run()
    {
        KillProcess();

        Console.WriteLine($"Programs closed successfully..........");
        Thread.Sleep(5000);

        RunNewProcess();
    }

    private void RunNewProcess()
    {
        if (string.IsNullOrEmpty(Argument))
        {
            RunNewProcess(ExePath);
        }
        RunNewProcess(ExePath, Argument);
    }

    public void RunTest()
    {
        KillProcess();
        Console.WriteLine("programs closed....");
        Thread.Sleep(50);
        Process start = new Process();
        Process.GetProcessesByName("Smart");
        start.Start();
        Console.WriteLine("---------------");
        Process.GetProcessesByName("Disport");
        start.Start();
        Console.WriteLine("opened the app...");
    }
}