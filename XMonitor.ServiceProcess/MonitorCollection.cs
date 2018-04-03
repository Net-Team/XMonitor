using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMonitor.ServiceProcess
{
    /// <summary>
    /// 表示监控的服务进程集合
    /// </summary>
    public class MonitorCollection : IEnumerable<ServiceProcessMonitor>
    {
        /// <summary>
        /// 监控的服务进程列表
        /// </summary>
        private readonly List<ServiceProcessMonitor> monitors = new List<ServiceProcessMonitor>();

        /// <summary>
        /// 获取监控服务进程的数量
        /// </summary>
        public int Count => this.monitors.Count;

        /// <summary>
        /// 添加服务进程
        /// </summary>
        /// <param name="alias">别名</param>
        /// <param name="serviceName">服务名称</param>
        public void Add(string alias, string serviceName)
        {
            var monitor = new ServiceProcessMonitor
            {
                Alias = alias,
                ServiceName = serviceName
            };
            this.monitors.Add(monitor);
        }

        /// <summary>
        /// 返回监控服务进程迭代器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<ServiceProcessMonitor> GetEnumerator()
        {
            return this.monitors.GetEnumerator();
        }

        /// <summary>
        /// 返回监控服务进程迭代器
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}