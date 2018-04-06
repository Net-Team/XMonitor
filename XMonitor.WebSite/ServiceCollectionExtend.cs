using XMonitor.WebSite;
using System;

namespace XMonitor.Core
{
    /// <summary>
    ///  ServiceCollection扩展
    /// </summary>
    public static class ServiceCollectionExtend
    {
        /// <summary>
        /// 使用http状态码监控服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options">配置选项</param>
        /// <returns></returns>
        public static MonitorCollection UseWebSiteMonitorService(this MonitorCollection services, Action<WebSiteOptions> options)
        {
            var opt = new WebSiteOptions();
            options?.Invoke(opt);

            var service = new WebSiteMonitorService(opt);
            services.Add(service);
            return services;
        }
    }
}
