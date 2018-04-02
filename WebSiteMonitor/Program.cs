using HttpStatusMonitor;
using MonitorServices;
using System;

namespace WebSiteMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.UseHttpStatusMonitor(opt =>
            {
                opt.UseEmailNotifyChannel(e =>
                {
                    e.TargetEmails.Add("qq@qq.com");
                    
                });
            });

            services.Start();
            Console.WriteLine("Hello WebSiteMonitor!");
        }
    }
}
