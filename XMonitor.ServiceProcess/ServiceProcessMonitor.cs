using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using XMonitor.Core;

namespace XMonitor.ServiceProcess
{
    /// <summary>
    /// 表示服务服务监控
    /// </summary>
    class ServiceProcessMonitor : Monitor<ServiceProcessOptions>
    {
        /// <summary>
        /// 获取服务信息
        /// </summary>
        public ServiceController Service { get; }

        /// <summary>
        /// 构造服务监控对象
        /// </summary>
        /// <param name="alias">服务别名</param>
        /// <param name="serviceName">服务名</param>
        /// <param name="options">服务选项</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public ServiceProcessMonitor(string alias, string serviceName, ServiceProcessOptions options)
            : base(options, alias, serviceName)
        {
            this.Service = ServiceController.GetServices().Where(item => item.ServiceName.Equals(serviceName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (this.Service == null)
            {
                throw new ArgumentException("服务不存在.", nameof(serviceName));
            }
        }

        /// <summary>
        /// 执行一次监控
        /// </summary>
        /// <returns></returns>
        protected override async Task OnCheckMonitorAsync()
        {
            try
            {
                this.Service.Refresh();
                if (this.Service.Status == ServiceControllerStatus.Stopped)
                {
                    base.Options.Logger?.Debug("服务被停止,正在恢复.");
                    this.Service.Start();
                    await base.NotifyAsync(new Exception("服务被停止,已恢复启动."));
                }
            }
            catch (Exception ex)
            {
                base.Options.Logger?.Error(ex);
            }
        }
    }
}
