using System;
using System.Collections.Generic;
using XMonitor.Core;

namespace XMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Action<IMonitorOptions> setCommonOptions = (opt =>
            {
                opt.AddEmailNotifyChannel(n =>
                {
                    n.Smtp = "mail.taichuan.com";
                    n.SenderAccout = "iot@taichaun.com";
                    n.SenderPassword = "tc123457";
                    n.TargetEmails.Add("tangfeng@taichuan.com");
                    n.Title = ctx => "v1.0" + ctx.Monitor.Alias;
                });

                opt.AddHttpNotifyChannel(n =>
                {
                    n.Uri = new Uri("http://www.baidu.com");
                    n.Header.Add(new KeyValuePair<string, string>("key", "value"));
                    n.Title = ctx => new KeyValuePair<string, string>("myTitle", ctx.Monitor.Alias + "v1.0");
                });
                opt.Logger = new ConsoleLogger();
            });

            var services = new MonitorCollection()
                .AddWebSiteMonitor("站点1", new Uri("http://iot.taichuan.net/404"), opt =>
                {
                    opt.HttpContentFilter = html => html != null && html.Contains("ok");
                    setCommonOptions(opt);
                })
                .AddServiceProcessMonitor("服务1", "serviceName", setCommonOptions)
                .AddProcessMonitor("程序1", "d:\\123.exe", setCommonOptions);

            services.Start();
            Console.WriteLine("Hello XMonitor!");
            Console.ReadLine();
        }
    }
}
