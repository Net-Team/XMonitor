using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiClient;
using XMonitor.Core;

namespace XMonitor.Web
{
    /// <summary>
    /// 网站监控服务
    /// </summary>
    public class WebMonitorService : IMonitorService, IDisposable
    {
        /// <summary>
        /// 选项
        /// </summary>
        private readonly WebOptions options;

        /// <summary>
        /// 获取或设置是否在运行
        /// </summary>
        public bool IsRunning { get; private set; }


        /// <summary>
        /// 监控对象
        /// </summary>
        public WebMonitorChecker monitorChecker { get; }

        /// <summary>
        /// 站点状态检测服务
        /// </summary>
        /// <param name="options">选项</param>
        /// <param name="monitorChecker">监控对象</param>
        public WebMonitorService(WebOptions options, WebMonitorChecker monitorChecker)
        {
            this.options = options;
            this.monitorChecker = monitorChecker;

        }

        /// <summary>
        /// 开始监控
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
            if (this.IsRunning == true)
            {

                try
                {
                    await monitorChecker.OnCheckMonitorAsync();
                }
                catch (HttpRequestException ex)
                {
                    await this.NotifyAsync(monitorChecker.Monitor, ex);
                }
                catch (Exception ex)
                {
                    this.options.Logger?.Error(ex);
                }
                await Task.Delay(this.options.Interval);
            }
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        {
            this.IsRunning = false;
            this.Dispose();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            monitorChecker.Dispose();
        }

        /// <summary>
        /// 通知异常
        /// </summary>
        /// <param name="monitor">产生异常的对象</param>
        /// <param name="exception">异常</param>
        /// <returns></returns>
        private async Task NotifyAsync(IMonitor monitor, HttpRequestException exception)
        {
            var context = new NotifyContext
            {
                Monitor = monitor,
                Exception = exception
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

    }
}
