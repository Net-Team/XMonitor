using XMonitor.ServiceProcess;
using System;

namespace XMonitor.Core
{
    /// <summary>
    /// 服务监控集合扩展
    /// </summary>
    public static class MonitorCollectionExtend
    {
        /// <summary>
        /// 添加服务状态监控
        /// </summary>
        /// <param name="monitor">服务监控集合</param>
        /// <param name="alias">服务别名</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="options">配置选项</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static MonitorCollection AddServiceProcessMonitor(this MonitorCollection monitor, string alias, string serviceName, Action<ServiceProcessOptions> options)
        {
            var opt = new ServiceProcessOptions();
            options?.Invoke(opt);

            var service = new ServiceProcessMonitor(opt, alias, serviceName);
            monitor.Add(service);
            return monitor;
        }
    }
}
