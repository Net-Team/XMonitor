using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMonitor.Core
{
    /// <summary>
    /// 定义监控的对象
    /// </summary>
    public interface IMonitor
    {
        /// <summary>
        /// 获取或设置别名
        /// </summary>
        string Alias { get; }

        /// <summary>
        /// 获取值
        /// </summary>
        object Value { get; }

        /// <summary>
        /// 启动服务
        /// </summary>
        void Start();

        /// <summary>
        /// 停止服务
        /// </summary>
        void Stop();    
    }
}
