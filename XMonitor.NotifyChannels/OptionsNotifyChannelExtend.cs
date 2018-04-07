using System;
using XMonitor.NotifyChannels;

namespace XMonitor.Core
{
    /// <summary>
    /// IOptions对象扩展
    /// </summary>
    public static class OptionsNotifyChannelExtend
    {
        /// <summary>
        /// 添加邮件通知 
        /// </summary>
        /// <param name="msOptions"></param>
        /// <param name="options">选项</param>
        public static void AddEmailNotifyChannel(this IMonitorOptions msOptions, Action<EmailChannelOptions> options)
        {
            var opt = new EmailChannelOptions();
            options?.Invoke(opt);

            var channel = new EmailChannel(opt);
            msOptions.NotifyChannels.Add(channel);
        }

        /// <summary>
        /// 添加Http通知 
        /// </summary>
        /// <param name="msOptions"></param>
        /// <param name="options">选项</param>
        public static void AddHttpNotifyChannel(this IMonitorOptions msOptions, Action<HttpChannelOptions> options)
        {
            var opt = new HttpChannelOptions();
            options?.Invoke(opt);

            var channel = new HttpChannel(opt);
            msOptions.NotifyChannels.Add(channel);
        }
    }
}
