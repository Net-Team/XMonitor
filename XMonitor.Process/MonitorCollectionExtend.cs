using System;
using XMonitor.Process;

namespace XMonitor.Core
{
    /// <summary>
    /// 进程监控集合扩展
    /// </summary>
    public static class MonitorCollectionExtend
    {
        /// <summary>
        /// 添加应用程序监控
        /// </summary>
        /// <param name="monitor">进程监控集合</param>
        /// <param name="alias">进程别名</param>
        /// <param name="fileName">进程文件路径</param>
        /// <param name="options">进程配置选项</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static MonitorCollection AddProcessMonitor(this MonitorCollection monitor, string alias, string fileName, Action<ProcessOptions> options)
        {
            return monitor.AddProcessMonitor(alias, new ProcessInfo(fileName), options);
        }

        /// <summary>
        /// 添加应用程序监控
        /// </summary>
        /// <param name="monitor">进程监控集合</param>
        /// <param name="alias">进程别名</param>
        /// <param name="processInfo">进程信息</param>
        /// <param name="options">进程配置选项</param>
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
