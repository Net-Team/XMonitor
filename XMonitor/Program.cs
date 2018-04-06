using XMonitor.Core;
using System;
using System.Collections.Generic;
using XMonitor.Web;
using XMonitor.NotifyChannels;

namespace XMonitor
{
    class Program
    {
        static void Main(string[] args)
        {


            Action<HttpChannelOptions> httpChannelOptions = (f =>
            {
                f.Uri = new Uri("http://www.baidu.com");
                f.Header.Add(new KeyValuePair<string, string>("key", "value"));
                f.Title = ctx => new KeyValuePair<string, string>("myTitle", ctx.Monitor.Alias + "v1.0");
            });

            Action<EmailChannelOptions> emailChannelOptions = (f =>
            {
                f.Smtp = "mail.taichuan.com";
                f.SenderAccout = "iot@taichaun.com";
                f.SenderPassword = "tc123457";
                f.TargetEmails.Add("tangfeng@taichuan.com");
                f.Title = ctx => "v1.0" + ctx.Monitor.Alias;
            });

            Action<WebOptions> webOpt = (f =>
            {
                f.UseEmailNotifyChannel(emailChannelOptions);
                f.UseHttpNotifyChannel(httpChannelOptions);
                f.Logger = new MonitorLoger();
            });

            var services = new MonitorCollection();
            services.AddWebMonitor("xx1网", new Uri("http://iot.taichuan.net/404"), webOpt);
            services.AddWebMonitor("xx2网", new Uri("http://iot.taichuan.net/405"), webOpt);

            services.AddServiceProcessMonitor("服务", "aaa", opt =>
            {
               
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
