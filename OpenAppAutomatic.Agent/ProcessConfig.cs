using System.Diagnostics;
using Serilog;

namespace OpenAppAutomatic;

public class ProcessHandler
{
    public void RunNewProcess(ProcessConfiguration processConfig)
    {
        var startInfo = new ProcessStartInfo(processConfig.ExePath, processConfig.Argument)
        {
            UseShellExecute = true, 
        };
        Process.Start(startInfo);
        
        Log.Information("Process Run Successfully");
    }

    public void KillProcess(ProcessConfiguration processConfig)
    {
        foreach (var process in Process.GetProcessesByName(processConfig.ProcessName))
        {
            process.Kill();
        }
        Log.Information("Process Has Been Closed");
    }
}