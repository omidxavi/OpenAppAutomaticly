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
        private const string configPath ="C:\\Users\\Omid\\RiderProjects\\OpenAppAutomatic\\ConsoleApp1\\bin\\Debug\\net6.0\\Config.csv";
        static void Main(string[] args) 
        {
            //Console.WriteLine("Enter hour that you want to start");
            //hour = Int32.Parse(Console.ReadLine());
            //Console.WriteLine("Enter minute that you want to start");
            //minute = Int32.Parse(Console.ReadLine());
            
            while (true)
            {
                if (IsInTime())
                {
                    Console.WriteLine("ok");
                    
                    // Run("C:/Ems/SmartTrader/Gateway/SmartTraderGateway.exe");
                    Run();
                }
                else
                {
                    Console.WriteLine("not in time");
                    Thread.Sleep(TimeSpan.FromMinutes(1));
                }
            }
        }
        private static void Run()
        {
            foreach (var process in Process.GetProcessesByName("Calculator"))
            {
                process.Kill();
            }
            var lines = File.ReadAllLines(configPath);
            var openPath = lines[0].Split(",")[0];
            Console.WriteLine("Program closed successfully..........");
            Thread.Sleep(10000);
            var startInfo = new ProcessStartInfo(openPath);
            startInfo.UseShellExecute = true;
            Process.Start(startInfo);
            
        }

        //private static readonly TimeSpan ScheduledTime = new TimeSpan(14,39 ,0);
        private static bool IsInTime()
        {
            var now = DateTime.Now.TimeOfDay;
            now = new TimeSpan(now.Hours, now.Minutes, 0);
            var lines = File.ReadAllLines(configPath);
            var setTime = lines[0].Split(",")[1];
            TimeSpan time = TimeSpan.Parse(setTime);
            if (now == time)
            {
                return true;
            }
            else
            {
                Console.WriteLine(now);
                return false;

            }

           
        }
    }
}
