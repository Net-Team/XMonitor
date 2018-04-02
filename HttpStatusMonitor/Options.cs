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
        /// 用于保存通知通道
        /// </summary>
        private readonly List<INotifyChannel> channels = new List<INotifyChannel>();

        /// <summary>
        /// 获取或设置检测的时间间隔
        /// </summary>
        public TimeSpan Interval { get; set; } = TimeSpan.FromMinutes(1d);

        /// <summary>
        /// 获取或设置响应内容过滤器
        /// </summary>
        public Func<string, bool> HttpContentFilter { get; set; }

        /// <summary>
        /// 获取或设置响应状态码过滤器
        /// </summary>
        public Func<HttpStatusCode, bool> HttpStatusFilter { get; set; }

        /// <summary>
        /// http状态码监控的配置项
        /// </summary>
        public Options()
        {
            this.HttpContentFilter = content => true;
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
            this.AddNotifyChannel(channel);
        }

        /// <summary>
        /// 添加通知通道
        /// </summary>
        /// <param name="channel">知通道</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddNotifyChannel(INotifyChannel channel)
        {
            if (channel == null)
            {
                throw new ArgumentNullException(nameof(channel));
            }
            this.channels.Add(channel);
        }
    }
}
