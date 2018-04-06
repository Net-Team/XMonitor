using XMonitor.Core;
using System;
using System.Collections.Generic;
using XMonitor.Web;

namespace XMonitor
{
    class Program
    {
        static void Main(string[] args)
        {


            TimeSpan ts = new TimeSpan();
            var services = new MonitorCollection();
            services.AddWebMonitor("xx网", new Uri("http://iot.taichuan.net/404"), opt =>
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

            //services.UseServiceProcessMonitorService(opt =>
            //{
            //    opt.Monitors.Add("xx服务", "aspnet_state");
            //    opt.UseEmailNotifyChannel(n =>
            //    {
            //        n.Smtp = "mail.taichuan.com";
            //        n.SenderAccout = "iot@taichaun.com";
            //        n.SenderPassword = "tc123457";
            //        n.TargetEmails.Add("tangfeng@taichuan.com");
            //        n.Title = ctx => "v1.0" + ctx.Monitor.Alias;
            //    });
            //    opt.Logger = new MonitorLoger();
            //});

            services.Start();
            Console.WriteLine("Hello WebSiteMonitor!");
            Console.ReadLine();
        }
    }
}
