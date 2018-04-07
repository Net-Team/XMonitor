using System;

namespace XMonitor.Core
{
    /// <summary>
    /// 表示监控到的异常基类
    /// 此异常用于通知
    /// </summary>
    public class MonitorException : Exception
    {
        /// <summary>
        /// 监控到的异常
        /// </summary>
        /// <param name="message">消息</param>
        public MonitorException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 监控到的异常
        /// </summary>
        /// <param name="inner">内部异常</param>
        /// <exception cref="ArgumentNullException"></exception>
        public MonitorException(Exception inner)
            : base(inner?.Message, inner ?? throw new ArgumentNullException(nameof(inner)))
        {
        }

        /// <summary>
        /// 监控到的异常
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="inner">内部异常</param>
        public MonitorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
