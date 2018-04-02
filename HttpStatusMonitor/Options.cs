using HttpStatusMonitor.NotifyChannels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace HttpStatusMonitor
{
    /// <summary>
    /// 表示http状态码监控的配置项
    /// </summary>
    public class Options
    {
        /// <summary>
        /// 获取或设置检测的时间间隔
        /// </summary>
        public TimeSpan Interval { get; set; } = TimeSpan.FromMinutes(1d);

        /// <summary>
        /// 请求超时时间
        /// 超时将处理为异常
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.FromMinutes(1d);

        /// <summary>
        /// 重试次数，当超过指定数次才定义为错误
        /// </summary>
        public int Retry { get; set; } = 3;

        /// <summary>
        /// 获取或设置响应内容过滤器
        /// </summary>
        public Func<string, bool> HttpContentFilter { get; set; }

        /// <summary>
        /// 获取或设置响应状态码过滤器
        /// </summary>
        public Func<HttpStatusCode, bool> HttpStatusFilter { get; set; }

        /// <summary>
        /// 获取目标url列表
        /// </summary>
        public List<Uri> TargetUrls => new List<Uri>();

        /// <summary>
        /// 获取通知通道列表
        /// </summary>
        public List<INotifyChannel> NotifyChannels => new List<INotifyChannel>();


        /// <summary>
        /// http状态码监控的配置项
        /// </summary>
        public Options()
        {
            this.HttpStatusFilter = this.IsSuccessStatusCode;
        }

        /// <summary>
        /// 是否为正确的状态码
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <returns></returns>
        private bool IsSuccessStatusCode(HttpStatusCode httpStatusCode)
        {
            var httpStatus = (int)httpStatusCode;
            return httpStatus >= 200 && httpStatus <= 299;
        }

        /// <summary>
        /// 使用邮件通知 
        /// </summary>
        /// <param name="options">选项</param>
        public void UseEmailNotifyChannel(Action<EmailChannelOptions> options)
        {
            var opt = new EmailChannelOptions();
            options?.Invoke(opt);

            var channel = new EmailChannel(opt);
            this.NotifyChannels.Add(channel);
        }
    }
}
