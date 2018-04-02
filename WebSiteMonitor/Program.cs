using HttpStatusMonitor;
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
                opt.UseEmailNotifyChannel(e =>
                {
                    e.Smtp = "mail.taichuan.com";
                    e.SenderAccout = "iot@taichaun.com";
                    e.SenderPassword = "tc123457";
                    e.TargetEmails.Add("tangfeng@taichuan.com");
                    e.TitleParameter = ctx => "v1.0" + ctx.SourceName;
                });
                opt.UseHttpNotifyChannel(e =>
                {
                    e.TargetUri = new Uri("http://www.baidu.com");
                    e.Header.Add(new KeyValuePair<string, string>("key", "value"));
                    e.TitleParameter = ctx => new KeyValuePair<string, string>("myTitle", ctx.SourceName + "v1.0");
                });
            });

            services.Start();
            Console.WriteLine("Hello WebSiteMonitor!");
            Console.ReadLine();
        }
    }
}
