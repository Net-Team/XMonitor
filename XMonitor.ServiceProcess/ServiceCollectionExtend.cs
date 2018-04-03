using XMonitor.ServiceProcess;
using System;

namespace XMonitor.Core
{
    /// <summary>
    ///  ServiceCollection扩展
    /// </summary>
    public static class ServiceCollectionExtend
    {
        /// <summary>
        /// 使用服务状态监控服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options">配置选项</param>
        /// <returns></returns>
        public static ServiceCollection UseServiceStatusMonitor(this ServiceCollection services, Action<ServiceOptions> options)
        {
            var opt = new ServiceOptions();
            options?.Invoke(opt);

            var service = new ServiceMonitorService(opt);
            services.Add(service);
            return services;
        }
    }
}
