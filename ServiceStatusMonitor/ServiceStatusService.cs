using MonitorServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStatusMonitor
{
    class ServiceStatusService : IMonitorService
    {
        /// <summary>
        /// 选项
        /// </summary>
        private readonly ServiceOptions options;
        /// <summary>
        /// 获取或设置是否在运行
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// 当前服务
        /// </summary>
        private ServiceController _service { get; set; }

        /// <summary>
        /// http状态检测服务
        /// </summary>
        /// <param name="options">选项</param>
        public ServiceStatusService(ServiceOptions options)
        {
            this.options = options;
        }


        public void Start()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取服务运行状态
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private bool CheckServiceStatus(string serviceName)
        {
            this._service = new ServiceController(serviceName);
            return this._service.Status != ServiceControllerStatus.Stopped;
        }

        /// <summary>
        /// 运行检测
        /// </summary>
        /// <returns></returns>
        private async Task RunAsync()
        {
            while (this.IsRunning == true)
            {
                foreach (var item in this.options.ServiceNames.Distinct())
                {
                    this.CheckServiceStatus(item);
                }
                await Task.Delay(this.options.Interval);
            }
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
