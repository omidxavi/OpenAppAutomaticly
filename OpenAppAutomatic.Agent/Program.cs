// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Timers;
using System.Xml;
using Timer = System.Timers.Timer;

namespace OpenAppAutomatic
{
    class Program
    {
        private static int hour;
        private static int minute;
        private static string configPath = Path.Combine(Directory.GetCurrentDirectory(), "Config.csv");
        static void Main(string[] args)
        {
            var configs = GetProcessesFromFile();
            
            while (true)
            {
                foreach (var processConfig in configs)
                {
                    if (processConfig.IsInTime())
                    {
                        processConfig.Run();
                    }
                    else
                    {
                        Console.WriteLine("not in time");
                        Thread.Sleep(TimeSpan.FromMinutes(1));
                    }
                }
            }
        }
        
        private static List<ProcessConfig> GetProcessesFromFile()
        {
            var configs = new List<ProcessConfig>();
            var lines = File.ReadAllLines(configPath);
            foreach (var line in lines.Where(l => !string.IsNullOrEmpty(l)))
            {
                configs.Add(new ProcessConfig(line));
            }

            return configs;
        }
    }
}