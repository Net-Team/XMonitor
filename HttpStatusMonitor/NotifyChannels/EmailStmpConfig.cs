using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpStatusMonitor.NotifyChannels
{
    /// <summary>
    /// 邮件Stmp 配置
    /// </summary>
    public class EmailStmpConfig
    {
        /// <summary>
        /// 邮件服务器
        /// </summary>
        public string Smtp { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }


        /// <summary>
        /// 是否SSL
        /// </summary>
        public bool SSL { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginAccout { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 构造邮件Stmp 配置
        /// </summary>
        /// <param name="smtp">邮件服务器Smtp</param>
        /// <param name="loginAccout">登录名</param>
        /// <param name="password">登录密码</param>
        /// <param name="port">端口</param>
        /// <param name="ssl">是否 SSl</param>
        public EmailStmpConfig(string smtp, string loginAccout, string password, int port = 25, bool ssl = false)
        {
            this.SSL = ssl;
            this.Port = port;
            this.Smtp = smtp;
            this.LoginAccout = loginAccout;
            this.Password = password;
        }
    }
}
