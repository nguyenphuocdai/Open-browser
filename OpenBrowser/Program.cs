using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace OpenBrowser
{
    internal class Program
    {
        public static Timer timer = new System.Timers.Timer
        {
            AutoReset = true,
            Enabled = true,
            Interval = TimeSpan.FromSeconds(300).TotalMilliseconds //15 seconds interval
        };

        static void Main(string[] args)
        {
            timer.Elapsed += OpenBrowser;
            timer.Start();

            Console.ReadLine();
        }
        public static void OpenBrowser(object? sender, ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                string url = "http://172.24.65.160:8000/";
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