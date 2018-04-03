using XMonitor.NotifyChannels;
using System;

namespace XMonitor.Core
{
    /// <summary>
    /// IOptions对象扩展
    /// </summary>
    public static class OptionsExtend
    {
        /// <summary>
        /// 使用邮件通知 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="options">选项</param>
        public static void UseEmailNotifyChannel(this IOptions o, Action<EmailChannelOptions> options)
        {
            var opt = new EmailChannelOptions();
            options?.Invoke(opt);

            var channel = new EmailChannel(opt);
            o.NotifyChannels.Add(channel);
        }

        /// <summary>
        /// 使用Http通知 
        /// </summary>
        /// <param name="o"></param>
        /// <param name="options">选项</param>
        public static void UseHttpNotifyChannel(this IOptions o, Action<HttpChannelOptions> options)
        {
            var opt = new HttpChannelOptions();
            options?.Invoke(opt);

            var channel = new HttpChannel(opt);
            o.NotifyChannels.Add(channel);
        }
    }
}
