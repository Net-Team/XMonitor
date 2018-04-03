using XMonitor.Core;

namespace XMonitor.ServiceProcess
{
    /// <summary>
    /// 表示监控的服务进程集合
    /// </summary>
    public class MonitorCollection : MonitorCollection<ServiceProcessMonitor>
    {
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
            base.Add(monitor);
        }
    }
}