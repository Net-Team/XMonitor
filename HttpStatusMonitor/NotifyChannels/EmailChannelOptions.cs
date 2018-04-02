using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpStatusMonitor.NotifyChannels
{
    /// <summary>
    /// 表示邮件通知选项
    /// </summary>
    public class EmailChannelOptions
    {
        /// <summary>
        /// 邮件服务器
        /// </summary>
        public string Smtp { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port => 25;

        /// <summary>
        /// 是否SSL
        /// </summary>
        public bool SSL => false;

        /// <summary>
        /// 发送者的邮箱账号
        /// </summary>
        public string SenderAccout { get; set; }

        /// <summary>
        /// 发送者的邮箱密码
        /// </summary>
        public string SenderPassword { get; set; }

        /// <summary>
        /// 接收者邮箱地址
        /// </summary>
        public List<string> TargetEmails => new List<string>();
    }
}
