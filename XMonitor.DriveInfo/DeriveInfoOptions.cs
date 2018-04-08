using System;
using System.Collections.Generic;
using XMonitor.Core;

namespace XMonitor.DriveInfo
{
    /// <summary>
    /// 表示磁盘监控的配置项
    /// </summary>
    public class DeriveInfoOptions : IMonitorOptions
    {
        /// <summary>
        /// 获取或设置日志工具
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 获取或设置检测的时间间隔
        /// 默认1分钟检测一次
        /// </summary>
        public TimeSpan Interval { get; set; } = TimeSpan.FromMinutes(1d);

        /// <summary>
        /// 获取通知通道列表
        /// </summary>
        public List<INotifyChannel> NotifyChannels { get; } = new List<INotifyChannel>();

        /// <summary>
        /// 最小空闲空间，单位MB
        /// 小于这个比例就报警
        /// 默认为1024MB
        /// </summary>
        public int MinFreeSpaceMB { get; set; } = 1024;
    }
}
