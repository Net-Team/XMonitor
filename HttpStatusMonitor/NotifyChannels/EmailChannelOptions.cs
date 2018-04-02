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
    }
}
