using MonitorServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpStatusMonitor
{
    /// <summary>
    /// 表示http状态检测服务
    /// </summary>
    class HttpStatusService : IMonitorService
    {
        /// <summary>
        /// 选项
        /// </summary>
        private readonly Options options;

        /// <summary>
        /// http状态检测服务
        /// </summary>
        /// <param name="options">选项</param>
        public HttpStatusService(Options options)
        {
            this.options = options;
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        public  void  Start()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
