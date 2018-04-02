using System;
using System.Threading.Tasks;

namespace HttpStatusMonitor.NotifyChannels
{
    /// <summary>
    /// 表示邮件通知
    /// </summary>
    class EmailChannel : INotifyChannel
    {
        /// <summary>
        /// 选项
        /// </summary>
        private readonly EmailChannelOptions opt;

        /// <summary>
        /// 邮件通知
        /// </summary>
        /// <param name="opt">选项</param>
        public EmailChannel(EmailChannelOptions opt)
        {
            this.opt = opt;
        }

        /// <summary>
        /// 通知异常内容
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="ex">异常</param>
        /// <returns></returns>

        public async Task NotifyAsync(string Title, Exception ex)
        {
            await this.opt.FromStmp.SendAsync(this.opt.TargetEmails, Title, ex.Message);
        }
    }
}
