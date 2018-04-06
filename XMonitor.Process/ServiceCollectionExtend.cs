using XMonitor.Process;
using System;

namespace XMonitor.Core
{
    /// <summary>
    ///  ServiceCollection扩展
    /// </summary>
    public static class ServiceCollectionExtend
    {
        /// <summary>
        /// 使用进程监控服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options">配置选项</param>
        /// <returns></returns>
        public static MonitorCollection UseProcessMonitorService(this MonitorCollection services, Action<ProcessOptions> options)
        {
            var opt = new ProcessOptions();
            options?.Invoke(opt);

            var service = new ProcessMonitorService(opt);
            services.Add(service);
            return services;
        }
    }
}
