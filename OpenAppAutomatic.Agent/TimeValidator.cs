namespace OpenAppAutomatic;

public class TimeValidator
{
    public List<ProcessConfiguration> validate(List<ProcessConfiguration> processes)
    {
        var validatedProcesses = new List<ProcessConfiguration>();
        
        foreach (var process in processes)
        {
            var isValid = IsValid(process);
            if (isValid)
            {
                validatedProcesses.Add(process);
            }
        }

        return validatedProcesses;
    }


    private bool IsValid(ProcessConfiguration process)
    {
        var now = DateTime.Now.TimeOfDay;
        now = new TimeSpan(now.Hours, now.Minutes, 0);
        
        if (now == process.ScheduleTime && !process.IsRunning)
        {
            process.IsRunning = true;
            return true;
        }
        else if (now > process.ScheduleTime)
        {
            process.IsRunning = false;
            return false;
        }
        else
        {
            return false;
        }
    }
}