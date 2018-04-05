using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMonitor.Core
{
    /// <summary>
    /// 定义监控服务选项接口
    /// </summary>
    public interface IMonitorServiceOptions
    {
        /// <summary>
        /// 获取或设置日志工具
        /// </summary>
        ILogger Logger { get; set; }

        /// <summary>
        /// 获取通知通道列表
        /// </summary>
        List<INotifyChannel> NotifyChannels { get; }

        /// <summary>
        /// 获取或设置检测的时间间隔
        /// </summary>
        TimeSpan Interval { get; set; }
    }
}
