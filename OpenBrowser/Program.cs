using System;
using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace OpenBrowser
{
    internal class Program
    {
        public static double Ti = TimeSpan.FromSeconds(300).TotalMilliseconds;
        public static string url = "http://172.24.65.160:8000/";
        public static Timer timer = new System.Timers.Timer
        {
            AutoReset = true,
            Enabled = true,
            Interval = Ti//15 seconds interval
        };

        static void Main(string[] args)
        {
            timer.Elapsed += OpenBrowser;
            timer.Start();
            Console.WriteLine($"{DateTime.Now.AddMilliseconds(Ti)}: Open browser {url}");
            Console.ReadLine();
        }
        public static void OpenBrowser(object? sender, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                Console.WriteLine($"{DateTime.Now}: Open browser {url}");
                var proc = Process.Start("C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe", url);
                Thread.Sleep(30000);

                if (!proc.HasExited)
                {
                    proc.Kill();
                    Console.WriteLine($"{DateTime.Now}: Close browser {url}");
                }
                else
                {
                    var alreadyRunningProc = Process.GetProcessesByName("chrome");
                    foreach (var chrome in alreadyRunningProc)
                    {
                        chrome.Kill();
                        Console.WriteLine($"{DateTime.Now}: Close browser {url}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine($"{DateTime.Now}: Restart timer");
                Console.WriteLine($"----------------------------------------------------------------------");
                timer.Start();
            }
        }
    }
}