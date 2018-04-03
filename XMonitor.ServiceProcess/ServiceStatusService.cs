using XMonitor.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace XMonitor.ServiceProcess
{
    /// <summary>
    /// 表示服务进程状态检测服务
    /// </summary>
    class ServiceStatusService : IMonitorService
    {
        /// <summary>
        /// 选项
        /// </summary>
        private readonly ServiceOptions options;

        /// <summary>
        /// 服务缓存
        /// </summary>
        private readonly ConcurrentDictionary<string, ServiceController> services;

        /// <summary>
        /// 获取或设置是否在运行
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// 服务进程状态检测服务
        /// </summary>
        /// <param name="options">选项</param>
        public ServiceStatusService(ServiceOptions options)
        {
            this.options = options;
            this.services = new ConcurrentDictionary<string, ServiceController>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 启动监控
        /// </summary>
        public async void Start()
        {
            this.IsRunning = true;
            await Task.Yield();
            await this.RunAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 运行检测
        /// </summary>
        /// <returns></returns>
        private async Task RunAsync()
        {
            while (this.IsRunning == true)
            {
                foreach (var monitor in this.options.Monitors)
                {
                    try
                    {
                        var service = this.GetServiceByName(monitor.Value);
                        if (service.Status == ServiceControllerStatus.Stopped)
                        {
                            this.options.Logger?.Debug("服务被停止,正在恢复.");
                            service.Start();
                            await this.NotifyAsync(monitor, new Exception("服务被停止,已恢复启动."));
                        }
                    }
                    catch (Exception ex)
                    {
                        await this.NotifyAsync(monitor, ex);
                        this.options.Logger?.Error(ex);
                    }
                }
                await Task.Delay(this.options.Interval);
            }
        }


        /// <summary>
        /// 通过服务名称查找服务
        /// </summary>
        /// <param name="name">服务名称</param>
        /// <returns></returns>
        private ServiceController GetServiceByName(string name)
        {
            var service = this.services.GetOrAdd(name, n => new ServiceController(n));
            service.Refresh();
            return service;
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

            foreach (var channel in this.options.NotifyChannels)
            {
                try
                {
                    await channel?.NotifyAsync(context);
                }
                catch (Exception ex)
                {
                    this.options.Logger?.Error(ex);
                }
            }
        }


        /// <summary>
        /// 停止监控
        /// </summary>
        public void Stop()
        {
            this.IsRunning = false;
        }
    }
}
