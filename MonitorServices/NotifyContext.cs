using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorServices
{
    /// <summary>
    /// 表示通知上下文
    /// </summary>
    public class NotifyContext
    {
        /// <summary>
        /// 获取消息源
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// 获取异常
        /// </summary>
        public Exception Exception { get; set; }
    }
}
