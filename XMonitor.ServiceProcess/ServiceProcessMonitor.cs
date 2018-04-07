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
        /// 获取服务名称
        /// </summary>
        public string ServiceName { get; private set; }

        /// <summary>
        /// 构造服务监控对象
        /// </summary>
        /// <param name="options">服务选项</param>
        /// <param name="alias">服务别名</param>
        /// <param name="serviceName">服务名</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public ServiceProcessMonitor(ServiceProcessOptions options, string alias, string serviceName)
            : base(options, alias, serviceName ?? throw new ArgumentNullException(serviceName))
        {
            this.ServiceName = serviceName;
        }

        /// <summary>
        /// 执行一次监控
        /// </summary>
        /// <returns></returns>
        protected override async Task OnCheckMonitorAsync()
        {
            try
            {
                var service = GetServiceByName(this.ServiceName);
                if (service.Status == ServiceControllerStatus.Stopped)
                {
                    base.Options.Logger?.Debug("服务已经被停止，正在恢复.");
                    service.Start();
                    base.Options.Logger?.Debug("服务已经被停止，已恢复启动..");
                }
            }
            catch (Exception ex)
            {
                await base.NotifyAsync(ex);
            }
        }


        /// <summary>
        /// 通过服务名获取服务信息
        /// </summary>
        /// <param name="serviceName">服务名</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        private static ServiceController GetServiceByName(string serviceName)
        {
            if (string.IsNullOrEmpty(serviceName))
            {
                throw new ArgumentNullException(nameof(serviceName));
            }

            var service = ServiceController
                .GetServices()
                .FirstOrDefault(s => string.Equals(s.ServiceName, serviceName, StringComparison.OrdinalIgnoreCase));

            if (service == null)
            {
                throw new ArgumentException($"服务{serviceName}不存在", nameof(serviceName));
            }
            return service;
        }

    }
}
