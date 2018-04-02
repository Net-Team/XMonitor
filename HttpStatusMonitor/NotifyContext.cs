using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpStatusMonitor
{
    /// <summary>
    /// 表示通知上下文
    /// </summary>
    public class NotifyContext
    {
        /// <summary>
        /// 获取配置选项
        /// </summary>
        public Options Options { get; internal set; }

        /// <summary>
        /// 获取目标URI
        /// </summary>
        public Uri TargetUrl { get; internal set; }

        /// <summary>
        /// 获取异常
        /// </summary>
        public Exception Exception { get; internal set; }
    }
}
