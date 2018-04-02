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
        /// 通知通道
        /// </summary>
        private readonly List<INotifyChannel> notifyChannels = new List<INotifyChannel>();


        /// <summary>
        /// 获取正确的状态集合
        /// </summary>
        public IEnumerable<HttpStatusCode> SuccessStatusCodes => new List<HttpStatusCode>();


        /// <summary>
        /// 获取或设置检测的时间间隔
        /// </summary>
        public TimeSpan Interval { get; set; } = TimeSpan.FromMinutes(1d);

        /// <summary>
        /// 使用邮件通知 
        /// </summary>
        /// <param name="options">选项</param>
        public void UseEmailNotifyChannel(Action<EmailChannelOptions> options)
        {
            var opt = new EmailChannelOptions();
            options?.Invoke(opt);

            var channel = new EmailChannel(opt);
            this.notifyChannels.Add(channel);
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
            this.notifyChannels.Add(channel);
        }
    }
}
