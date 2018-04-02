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
                opt.TargetUrls.Add(new Uri("http://iot.taichuan.net/404"));
                opt.UseEmailNotifyChannel(e =>
                {
                    e.Smtp = "mail.taichuan.com";
                    e.SenderAccout = "iot@taichaun.com";
                    e.SenderPassword = "tc123457";
                    e.TargetEmails.Add("tangfeng@taichuan.com");
                });
            });

            services.Start();
            Console.WriteLine("Hello WebSiteMonitor!");
            Console.ReadLine();
        }
    }
}
