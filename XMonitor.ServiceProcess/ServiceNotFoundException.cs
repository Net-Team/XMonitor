using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMonitor.Core;

namespace XMonitor.ServiceProcess
{
    /// <summary>
    /// 表示服务不存在异常
    /// </summary>
    class ServiceNotFoundException : MonitorException
    {
        /// <summary>
        /// 服务不存在异常
        /// </summary>
        /// <param name="serviceName"></param>
        public ServiceNotFoundException(string serviceName)
            : base($"服务{serviceName}不存在")
        {
        }
    }
}
