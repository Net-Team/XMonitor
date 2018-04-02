using MonitorServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;
using WebApiClient.Contexts;
using WebApiClient.Parameterables;

namespace HttpStatusMonitor
{
    /// <summary>
    /// 定义Http状态接口
    /// </summary>
    public interface IHttpStatusApi : IDisposable
    {
        /// <summary>
        /// 检测指定URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        [HttpGet]
        ITask<HttpResponseMessage> CheckAsync([Url] Uri url, Timeout timeout);
    }

    /// <summary>
    /// 表示http状态检测服务
    /// </summary>
    class HttpStatusService : IMonitorService, IDisposable
    {
        /// <summary>
        /// 选项
        /// </summary>
        private readonly Options options;

        /// <summary>
        /// api客户端
        /// </summary>
        private readonly IHttpStatusApi httpStatusApi;

        /// <summary>
        /// 获取或设置是否在运行
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// http状态检测服务
        /// </summary>
        /// <param name="options">选项</param>
        public HttpStatusService(Options options)
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
            await this.RunAsync();
        }

        /// <summary>
        /// 运行检测
        /// </summary>
        /// <returns></returns>
        private async Task RunAsync()
        {
            while (this.IsRunning == true)
            {
                try
                {
                    await this.CheckHttpStatusAsync();
                }
                catch (Exception ex)
                {
                    foreach (var item in this.options.NotifyChannels)
                    {
                        await item.NotifyAsync(ex);
                    }
                }
                await Task.Delay(this.options.Interval);
            }
        }

        /// <summary>
        /// 检测远程http服务状态
        /// </summary>
        /// <returns></returns>
        private async Task CheckHttpStatusAsync()
        {
            foreach (var url in this.options.TargetUrls)
            {
                if (url != null)
                {
                    await this.httpStatusApi
                        .CheckAsync(url, this.options.Timeout)
                        .Retry(this.options.Retry)
                        .WhenCatch<HttpRequestException>();
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

        /// <summary>
        /// http状态过滤器
        /// </summary>
        class HttpStatusFilter : ApiActionFilterAttribute
        {
            /// <summary>
            /// 选项
            /// </summary>
            private readonly Options options;

            /// <summary>
            /// http状态过滤器
            /// </summary>
            /// <param name="options"></param>
            public HttpStatusFilter(Options options)
            {
                this.options = options;
            }

            public override async Task OnEndRequestAsync(ApiActionContext context)
            {
                await base.OnEndRequestAsync(context);
                var response = context.ResponseMessage;

                if (this.options.HttpStatusFilter?.Invoke(response.StatusCode) == false ||
                    this.options.HttpContentFilter?.Invoke(await response.Content.ReadAsStringAsync()) == false)
                {
                    var ex = new Exception("远程服务器响应状态或内容不符合预期");
                    throw new HttpFailureStatusException(response.StatusCode, context, ex);
                }
            }
        }
    }
}
