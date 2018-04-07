using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiClient;
using XMonitor.Core;

namespace XMonitor.WebSite
{
    /// <summary>
    /// 表示站点对象
    /// </summary>
    public class WebSiteMonitor : Monitor<WebSiteOptions>
    {
        /// <summary>
        /// api客户端
        /// </summary>
        private readonly IHttpStatusApi httpStatusApi;

        /// <summary>
        /// 获取网址
        /// </summary>
        public Uri Uri { get; }

        /// <summary>
        /// 构造站点监控对象
        /// </summary>
        /// <param name="options">监控选项</param>
        /// <param name="alias">站点名称</param>
        /// <param name="uri">站点地址</param>
        /// <exception cref="ArgumentNullException"></exception>
        public WebSiteMonitor(WebSiteOptions options, string alias, Uri uri)
            : base(options, alias, uri = uri ?? throw new ArgumentNullException(nameof(uri)))
        {
            this.Uri = uri;
            var config = new HttpApiConfig();
            config.GlobalFilters.Add(new HttpStatusFilter(options));
            this.httpStatusApi = HttpApiClient.Create<IHttpStatusApi>(config);
        }

        /// <summary>
        /// 执行一次监控
        /// </summary>
        /// <returns></returns>
        protected override async Task OnCheckMonitorAsync()
        {
            await this.httpStatusApi
                .CheckAsync(this.Uri, base.Options.Timeout)
                .Retry(base.Options.Retry)
                .WhenCatch<HttpRequestException>();
        }

        /// <summary>
        /// 检测异常通知
        /// </summary>
        /// <param name="ex">异常</param>
        protected override async Task OnCheckExceptionAsync(Exception ex)
        {
            await base.NotifyAsync(ex);
        }
    }
}
