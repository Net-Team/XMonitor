using XMonitor.Process;
using System;

namespace XMonitor.Core
{
    /// <summary>
    /// 应用程序监控集合扩展
    /// </summary>
    public static class MonitorCollectionExtend
    {
        /// <summary>
        /// 添加应用程序监控
        /// </summary>
        /// <param name="monitor">应用程序监控集合</param>
        /// <param name="alias">应用程序别名</param>
        /// <param name="processInfo">应用程序信息</param>
        /// <param name="options">应用程序配置选项</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static MonitorCollection AddProcessMonitor(this MonitorCollection monitor, string alias, ProcessInfo processInfo, Action<ProcessOptions> options)
        {
            var opt = new ProcessOptions();
            options?.Invoke(opt);

            var service = new ProcessMonitor(opt, alias, processInfo);
            monitor.Add(service);
            return monitor;
        }
    }
}
