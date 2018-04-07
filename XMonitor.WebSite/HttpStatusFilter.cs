using System;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;
using WebApiClient.Contexts;

namespace XMonitor.WebSite
{
    /// <summary>
    /// http状态过滤器
    /// </summary>
    class HttpStatusFilter : ApiActionFilterAttribute
    {
        /// <summary>
        /// 选项
        /// </summary>
        private readonly WebSiteOptions options;

        /// <summary>
        /// http状态过滤器
        /// </summary>
        /// <param name="options"></param>
        public HttpStatusFilter(WebSiteOptions options)
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
