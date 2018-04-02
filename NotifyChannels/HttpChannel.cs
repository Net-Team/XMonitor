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
    /// 异常通知
    /// </summary>
    public class HttpChannel : INotifyChannel
    {
        /// <summary>
        /// 异常通知接口
        /// </summary>
        private readonly ISendApi SendApi = HttpApiClient.Create<ISendApi>();

        /// <summary>
        /// 选项
        /// </summary>
        private readonly HttpChannelOptions opts;

        /// <summary>
        /// Http 异常通知
        /// </summary>
        /// <param name="opt">选项</param>
        public HttpChannel(HttpChannelOptions opt)
        {
            this.opts = opt;
        }

        /// <summary>
        /// Http 异常通知
        /// </summary>
        /// <param name="context">通知上下文</param>
        /// <returns></returns>
        public async Task NotifyAsync(NotifyContext context)
        {
            var dic = new List<KeyValuePair<string, string>>();
            dic.Add(this.opts.TitleParameter(context));
            dic.Add(this.opts.MessageParameter(context));

            await SendApi
                .SendAsync(this.opts.TargetUri, this.opts.Header, dic)
                .HandleAsDefaultWhenException();
        }
    }

    /// <summary>
    /// Http 异常通知接口
    /// </summary>
    public interface ISendApi : IDisposable
    {
        /// <summary>
        /// 发送Http请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="header">请求头</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        [HttpPost]
        ITask<HttpResponseMessage> SendAsync(
            [Url] Uri url,
            [Headers] IEnumerable<KeyValuePair<string, string>> header,
            [FormContent] List<KeyValuePair<string, string>> content
            );
    }
}
