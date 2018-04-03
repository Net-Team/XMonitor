using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMonitor.Core;

namespace XMonitor.ServiceProcess
{
    /// <summary>
    /// 表示服务进程对象
    /// </summary>
    public class ServiceProcessMonitor : IMonitor
    {
        /// <summary>
        /// 获取或设置别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// 获取或设置服务进程名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 获取或设置网址
        /// </summary>
        object IMonitor.Value
        {
            get
            {
                return this.ServiceName;
            }
        }
    }
}
