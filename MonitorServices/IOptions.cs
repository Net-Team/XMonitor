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
    public interface IOptions
    {
        /// <summary>
        /// 获取或设置日志工具
        /// </summary>
        ILogger Logger { get; set; }

        /// <summary>
        /// 获取通知通道列表
        /// </summary>
        List<INotifyChannel> NotifyChannels { get; }
    }

    /// <summary>
    /// 定义监控服务选项接口
    /// </summary>
    public interface IMonitorOptions<TValue> : IOptions
    {
        /// <summary>
        /// 获取监控目标的集合
        /// </summary>
        MonitorCollection<TValue> Monitors { get; }
    }
}
