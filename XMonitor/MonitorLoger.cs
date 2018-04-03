using XMonitor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMonitor
{
    /// <summary>
    /// 监控日志
    /// </summary>
    public class MonitorLoger : ILogger
    {
        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="message">消息</param>
        public void Debug(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="ex"></param>
        public void Error(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
