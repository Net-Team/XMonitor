using System;
using System.Collections.Generic;
using XMonitor.Core;
using XMonitor.NotifyChannels;

namespace XMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<IMonitorOptions> UseNotify = (opt =>
            {
                opt.UseEmailNotifyChannel(n =>
                {
                    n.Smtp = "mail.taichuan.com";
                    n.SenderAccout = "iot@taichaun.com";
                    n.SenderPassword = "tc123457";
                    n.TargetEmails.Add("tangfeng@taichuan.com");
                    n.Title = ctx => "v1.0" + ctx.Monitor.Alias;
                });

                opt.UseHttpNotifyChannel(n =>
                {
                    n.Uri = new Uri("http://www.baidu.com");
                    n.Header.Add(new KeyValuePair<string, string>("key", "value"));
                    n.Title = ctx => new KeyValuePair<string, string>("myTitle", ctx.Monitor.Alias + "v1.0");
                });
                opt.Logger = new MonitorLoger();
            });

            var services = new MonitorCollection()
                .AddWebSiteMonitor("站点1", new Uri("http://iot.taichuan.net/404"), opt =>
                {
                    UseNotify(opt);
                })
                .AddServiceProcessMonitor("服务1", "serviceName", opt =>
                {
                    UseNotify(opt);
                });

            services.Start();
            Console.WriteLine("Hello XMonitor!");
            Console.ReadLine();
        }
    }
}
