using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMonitor.Core
{
    /// <summary>
    /// 定义监控服务接口
    /// </summary>
    public interface IMonitorService
    {
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
