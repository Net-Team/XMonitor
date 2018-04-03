using XMonitor.Core;
using System;
using System.Collections.Generic;

namespace XMonitor.Process
{
    /// <summary>
    /// 表示进程监控的配置项
    /// </summary>
    public class ProcessOptions : IMonitorServiceOptions
    {
        /// <summary>
        /// 获取或设置日志工具
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 获取监控的进程列表
        /// </summary>
        public MonitorCollection Monitors { get; } = new MonitorCollection();

        /// <summary>
        /// 获取通知通道列表
        /// </summary>
        public List<INotifyChannel> NotifyChannels { get; } = new List<INotifyChannel>();
    }
}
