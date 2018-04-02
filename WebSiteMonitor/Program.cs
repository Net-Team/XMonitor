using HttpStatusMonitor;
using ServiceStatusMonitor;
using MonitorServices;
using System;
using System.Collections.Generic;

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
                opt.UseEmailNotifyChannel(n =>
                {   
                    n.Smtp = "mail.taichuan.com";
                    n.SenderAccout = "iot@taichaun.com";
                    n.SenderPassword = "tc123457";
                    n.TargetEmails.Add("tangfeng@taichuan.com");
                    n.Title = ctx => "v1.0" + ctx.SourceName;
                });
                opt.UseHttpNotifyChannel(n =>
                {
                    n.TargetUri = new Uri("http://www.baidu.com");
                    n.Header.Add(new KeyValuePair<string, string>("key", "value"));
                    n.Title = ctx => new KeyValuePair<string, string>("myTitle", ctx.SourceName + "v1.0");
                });
            });

            services.UseServiceStatusMonitor(opt =>
            {
                opt.ServiceNames.Add("aspnet_state");
                opt.UseEmailNotifyChannel(n =>
                {
                    n.Smtp = "mail.taichuan.com";
                    n.SenderAccout = "iot@taichaun.com";
                    n.SenderPassword = "tc123457";
                    n.TargetEmails.Add("tangfeng@taichuan.com");
                    n.Title = ctx => "v1.0" + ctx.SourceName;
                });
            });

            services.Start();
            Console.WriteLine("Hello WebSiteMonitor!");
            Console.ReadLine();
        }
    }
}
