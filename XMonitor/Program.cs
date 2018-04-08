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
                    n.TargetEmails.Add("chenguowei@taichuan.com");
                });

                opt.AddHttpNotifyChannel(n =>
                {
                    n.Uri = new Uri("http://www.baidu.com");
                    n.Header.Add(new KeyValuePair<string, string>("key", "value"));
                });
                opt.Logger = new ConsoleLogger();
            });

            var services = new MonitorCollection()
                .AddServiceProcessMonitor("服务1", "serviceName", setCommonOptions)
                .AddProcessMonitor("程序1", "d:\\123.exe", setCommonOptions)
                .AddDriveInfoMonitor("C盘", "C", opt =>
                {
                    opt.MinFreeSpaceMB = 500;
                    setCommonOptions(opt);
                })
                .AddWebSiteMonitor("站点1", new Uri("http://iot.taichuan.net/404"), opt =>
                {
                    opt.HttpContentFilter = html => html != null && html.Contains("ok");
                    setCommonOptions(opt);
                });

            services.Start();
            Console.WriteLine("Hello XMonitor!");
            Console.ReadLine();
        }
    }
}
