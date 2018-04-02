using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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
        /// <param name="ex">异常</param>
        /// <returns></returns>

        public async Task NotifyAsync(Exception ex)
        {
            await this.SendAsync(ex.Message);
        }


        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="body">内容</param>
        /// <returns></returns>
        public async Task SendAsync(string body)
        {
            var msg = new MailMessage
            {
                From = new MailAddress(this.opt.LoginAccout),
                Subject = this.opt.Title,
                SubjectEncoding = Encoding.UTF8,
                Body = body,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true,
            };

            foreach (var item in this.opt.TargetEmails.Distinct())
            {
                if (string.IsNullOrEmpty(item) == false && Regex.IsMatch(item, @"^\w+(\.\w*)*@\w+\.\w+$"))
                {
                    msg.To.Add(item);
                }
            }

            if (msg.To.Count == 0)
            {
                return;
            }

            using (var client = new SmtpClient())
            {
                client.Credentials = new NetworkCredential(this.opt.LoginAccout, this.opt.Password);
                client.Port = this.opt.Port;
                client.Host = this.opt.Smtp;
                client.EnableSsl = this.opt.SSL;
                await client.SendMailAsync(msg);
            }
        }
    }
}
