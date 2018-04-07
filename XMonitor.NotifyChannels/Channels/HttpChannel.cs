using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiClient;
using XMonitor.Core;

namespace XMonitor.NotifyChannels
{
    /// <summary>
    /// 表示Http异常通知通道
    /// </summary>
    class HttpChannel : INotifyChannel
    {
        /// <summary>
        /// 选项
        /// </summary>
        private readonly HttpChannelOptions opt;

        /// <summary>
        /// 异常通知接口
        /// </summary>
        private readonly IHttpNotifyClient httpNotifyClient = HttpApiClient.Create<IHttpNotifyClient>();

        /// <summary>
        /// Http异常通知通道
        /// </summary>
        /// <param name="opt">选项</param>
        public HttpChannel(HttpChannelOptions opt)
        {
            this.opt = opt;
        }

        /// <summary>
        /// Http 异常通知
        /// </summary>
        /// <param name="context">通知上下文</param>
        /// <returns></returns>
        public async Task NotifyAsync(NotifyContext context)
        {
            var httpContent = new List<KeyValuePair<string, string>>
            {
                this.opt.Title(context),
                this.opt.Message(context)
            };

            await this.httpNotifyClient
                .SendNotifyAsync(this.opt.Uri, this.opt.Header, httpContent);
        }
    }
}
