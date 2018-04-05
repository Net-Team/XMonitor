using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebApiClient;
using XMonitor.Core;

namespace XMonitor.Web
{
    /// <summary>
    /// 执行监控网站
    /// </summary>
    public class WebMonitorChecker : MonitorChecker<WebMonitor>
    {
        /// <summary>
        /// api客户端
        /// </summary>
        private IHttpStatusApi httpStatusApi;

        /// <summary>
        /// 选项
        /// </summary>
        private readonly WebOptions opt;

        /// <summary>
        /// 构建Web监控
        /// </summary>
        /// <param name="monitor"></param>
        /// <param name="options"></param>
        public WebMonitorChecker(WebMonitor monitor, WebOptions options)
            : base(monitor, options)
        {
            this.opt = options;
        }

        /// <summary>
        /// 执行监控
        /// </summary>
        /// <returns></returns>
        public override async Task OnCheckMonitorAsync()
        {
            await this.CheckHttpStatusAsync(base.Monitor.Uri);
        }


        /// <summary>
        /// 执行监控
        /// 异常输出
        /// </summary>
        /// <param name="ex">异常消息</param>
        /// <returns></returns>
        public override void OnCheckException(Exception ex)
        {
            opt.Logger.Debug(ex.Message);
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
                var config = new HttpApiConfig();
                config.GlobalFilters.Add(new HttpStatusFilter(this.opt));
                this.httpStatusApi = HttpApiClient.Create<IHttpStatusApi>(config);

                await this.httpStatusApi
                    .CheckAsync(uri, this.opt.Timeout)
                    .Retry(this.opt.Retry)
                    .WhenCatch<HttpRequestException>();
            }
        }
    }
}
