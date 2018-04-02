using System;

namespace WebSiteMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddExceptionService(opt =>
            {
                opt.UseEmailNotifyChannel(e => e.TargetEmail = "qq@qq.com");
            });

            foreach (var item in services)
            {
                item.StartAsync();
            }

            Console.WriteLine("Hello World!");
        }
    }
}
