using System.Diagnostics;

namespace OpenAppAutomatic;

public class ProcessHandler
{
    public void RunNewProcess(ProcessConfig processConfig)
    {
        var startInfo = new ProcessStartInfo(processConfig.ExePath, processConfig.Argument)
        {
            UseShellExecute = true, 
        };
        Process.Start(startInfo);
        
        Console.WriteLine("Process Run Successfully");
    }

    public void KillProcess(ProcessConfig processConfig)
    {
        foreach (var process in Process.GetProcessesByName(processConfig.ProcessName))
        {
            process.Kill();
        }
    }

}