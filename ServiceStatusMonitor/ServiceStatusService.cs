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


        public async void Start()
        {
            this.IsRunning = true;
            await this.RunAsync();
        }

        /// <summary>
        /// 获取服务运行状态
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        private bool CheckServiceStatus(string serviceName)
        {
            this._service = new ServiceController(serviceName);
            this._service.Refresh();
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
                foreach (var item in this.options.Monitors)
                {
                    try
                    {
                        if (this.CheckServiceStatus(item.Value) == false)
                        {
                            this.options.Logger?.Debug("服务被停止,正在恢复.");
                            this._service.Start();
                            await this.NotifyAsync(item, new Exception("服务被停止,已恢复启动."));
                        }
                    }
                    catch (Exception ex)
                    {
                        await this.NotifyAsync(item, ex);
                        this.options.Logger?.Error(ex);
                    }
                }
                await Task.Delay(this.options.Interval);
            }
        }

        /// <summary>
        /// 停止监控
        /// </summary>
        public void Stop()
        {
            this.IsRunning = false;
        }

        /// <summary>
        /// 通知异常
        /// </summary>
        /// <param name="monitor">产生异常的服务</param>
        /// <param name="exception">异常</param>
        /// <returns></returns>
        private async Task NotifyAsync(IMonitor<string> monitor, Exception exception)
        {
            var context = new NotifyContext
            {
                Monitor = monitor,
                Exception = exception,
            };

            foreach (var item in this.options.NotifyChannels)
            {
                try
                {
                    await item?.NotifyAsync(context);
                }
                catch (Exception ex)
                {
                    this.options.Logger?.Error(ex);
                }
            }
        }
    }
}
