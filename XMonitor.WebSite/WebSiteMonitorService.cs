using XMonitor.Core;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebApiClient;

namespace XMonitor.WebSite
{
    /// <summary>
    /// 表示站点状态检测服务
    /// </summary>
    class WebSiteMonitorService : IMonitorService, IDisposable
    {
        /// <summary>
        /// 选项
        /// </summary>
        private readonly WebSiteOptions options;

        /// <summary>
        /// api客户端
        /// </summary>
        private readonly IHttpStatusApi httpStatusApi;

        /// <summary>
        /// 获取或设置是否在运行
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// 站点状态检测服务
        /// </summary>
        /// <param name="options">选项</param>
        public WebSiteMonitorService(WebSiteOptions options)
        {
            this.options = options;
            var config = new HttpApiConfig();
            config.GlobalFilters.Add(new HttpStatusFilter(options));
            this.httpStatusApi = HttpApiClient.Create<IHttpStatusApi>(config);
        }

        /// <summary>
        /// 启动服务
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
                        await this.CheckHttpStatusAsync(monitor.Uri);
                    }
                    catch (HttpRequestException ex)
                    {
                        await this.NotifyAsync(monitor, ex);
                    }
                    catch (Exception ex)
                    {
                        this.options.Logger?.Error(ex);
                    }
                }

                await Task.Delay(this.options.Interval);
            }
        }


        /// <summary>
        /// 检测远程http服务状态
        /// </summary>
        /// <param name="uri">目标地址</param>
        /// <returns></returns>
        private async Task CheckHttpStatusAsync(Uri uri)
        {
            if (uri != null)
            {
                await this.httpStatusApi
                    .CheckAsync(uri, this.options.Timeout)
                    .Retry(this.options.Retry)
                    .WhenCatch<HttpRequestException>();
            }
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

        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        {
            this.IsRunning = false;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            this.httpStatusApi.Dispose();
        }
    }
}
