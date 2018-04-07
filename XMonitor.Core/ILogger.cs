using System;

namespace XMonitor.Core
{
    /// <summary>
    /// 表示日志
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 异常日志
        /// </summary>
        /// <param name="ex">异常</param>
        void Error(Exception ex);

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="message">消息</param>
        void Debug(string message);
    }
}
