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
    /// 网站监控
    /// </summary>
    public class WebMonitor : Monitor
    {
        /// <summary>
        /// 获取或设置网址
        /// </summary>
        public Uri Uri { get; set; }


        /// <summary>
        /// 网站监控选项
        /// </summary>
        private readonly WebOptions opt;

        /// <summary>
        /// api客户端
        /// </summary>
        private readonly IHttpStatusApi httpStatusApi;

        /// <summary>
        /// 获取或设置是否在运行
        /// </summary>
        public bool IsRunning { get; private set; }


        /// <summary>
        /// 构造监控对象
        /// </summary>
        /// <param name="alias">网站名称</param>
        /// <param name="uri">网站地址</param>
        /// <param name="options">监控选项</param>
        public WebMonitor(string alias, Uri uri, WebOptions options)
            : base(alias, uri, options)
        {
            this.opt = options;
            this.Uri = uri;
            var config = new HttpApiConfig();
            config.GlobalFilters.Add(new HttpStatusFilter(options));
            this.httpStatusApi = HttpApiClient.Create<IHttpStatusApi>(config);
        }

        /// <summary>
        /// 执行检测
        /// </summary>
        /// <returns></returns>
        public override async Task OnCheckMonitorAsync()
        {
            if (this.Uri != null)
            {
                await this.httpStatusApi
                    .CheckAsync(this.Uri, this.opt.Timeout)
                    .Retry(this.opt.Retry)
                    .WhenCatch<HttpRequestException>();
            }
        }

        /// <summary>
        /// 检测异常，输出日志
        /// </summary>
        /// <param name="ex"></param>
        protected override async Task OnCheckExceptionAsync(Exception ex)
        {
            this.opt.Logger.Debug(ex.Message);
            await this.NotifyAsync(ex);
        }

        /// <summary>
        /// 通知异常
        /// </summary>
        /// <param name="exception">异常</param>
        /// <returns></returns>
        private async Task NotifyAsync(HttpRequestException exception)
        {
            var context = new NotifyContext
            {
                Monitor = this,
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
