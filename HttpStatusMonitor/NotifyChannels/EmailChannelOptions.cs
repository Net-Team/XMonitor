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
        /// 目标邮箱地址
        /// </summary>
        public List<string> TargetEmails => new List<string>();

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
        /// 登录名
        /// </summary>
        public string LoginAccout { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 通知标题
        /// </summary>
        public string Title { get; set; }

    }
}
