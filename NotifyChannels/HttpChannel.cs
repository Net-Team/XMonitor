using MonitorServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;

namespace NotifyChannels
{
    /// <summary>
    /// 表示Http异常通知通道
    /// </summary>
    public class HttpChannel : INotifyChannel
    {
        /// <summary>
        /// Http 异常通知接口
        /// </summary>
        public interface IHttpNotifyClient : IDisposable
        {
            /// <summary>
            /// 发送Http请求
            /// </summary>
            /// <param name="url"></param>
            /// <param name="header">请求头</param>
            /// <param name="content">内容</param>
            /// <returns></returns>
            [HttpPost]
            ITask<HttpResponseMessage> SendNotifyAsync(
                [Url] Uri url,
                [Headers] IEnumerable<KeyValuePair<string, string>> header,
                [FormContent] IEnumerable<KeyValuePair<string, string>> content);
        }


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
            var httpContent = new List<KeyValuePair<string, string>>();
            httpContent.Add(this.opt.Title(context));
            httpContent.Add(this.opt.Message(context));

            await this.httpNotifyClient
                .SendNotifyAsync(this.opt.TargetUri, this.opt.Header, httpContent)
                .HandleAsDefaultWhenException();
        }
    }
}
