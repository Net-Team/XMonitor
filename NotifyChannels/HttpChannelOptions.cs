using MonitorServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotifyChannels
{
    /// <summary>
    /// Http 消息通知选项
    /// </summary>
    public class HttpChannelOptions
    {
        /// <summary>
        /// 接收者接口地址
        /// </summary>
        public Uri TargetUri { get; set; }

        /// <summary>
        /// 获取请求头
        /// </summary>
        public List<KeyValuePair<string, string>> Header { get; } = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// Http参数 
        /// 消息标题参数委托
        /// </summary>
        public Func<NotifyContext, KeyValuePair<string, string>> TitleParameter { get; set; } = ctx => new KeyValuePair<string, string>("Title", ctx.SourceName);

        /// <summary>
        /// Http参数 
        /// 消息内容委托
        /// </summary>
        public Func<NotifyContext, KeyValuePair<string, string>> MessageParameter { get; set; } = ctx => new KeyValuePair<string, string>("Message", ctx.Exception.Message);
    }
}
