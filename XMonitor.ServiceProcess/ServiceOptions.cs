using XMonitor.Core;
using System;
using System.Collections.Generic;

namespace XMonitor.ServiceProcess
{
    /// <summary>
    /// 表示服务状态码监控的配置项
    /// </summary>
    public class ServiceOptions : IMonitorOptions<string>
    {
        /// <summary>
        /// 获取或设置日志工具
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 获取或设置检测的时间间隔
        /// 默认1分钟
        /// </summary>
        public TimeSpan Interval { get; set; } = TimeSpan.FromMinutes(1d);

        /// <summary>
        /// 获取监控的服务列表
        /// </summary>
        public MonitorCollection<string> Monitors { get; } = new MonitorCollection<string>();

        /// <summary>
        /// 获取通知通道列表
        /// </summary>
        public List<INotifyChannel> NotifyChannels { get; } = new List<INotifyChannel>();
    }
}
