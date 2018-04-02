using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorServices
{
    /// <summary>
    /// 定义监控的对象
    /// </summary>
    public interface IMonitor
    {
        /// <summary>
        /// 获取或设置别名
        /// </summary>
        string Alias { get; set; }

        /// <summary>
        /// 获取或设置值
        /// </summary>
        object Value { get; set; }
    }

    /// <summary>
    /// 定义监控的对象
    /// </summary>
    public interface IMonitor<TValue> : IMonitor
    {
        /// <summary>
        /// 获取或设置值
        /// </summary>
        new TValue Value { get; set; }
    }
}
