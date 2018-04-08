using System;
using System.Text;

namespace XMonitor.Core
{
    /// <summary>
    /// 表示通知上下文
    /// </summary>
    public class NotifyContext
    {
        /// <summary>
        /// 获取或设置监控对象
        /// </summary>
        public IMonitor Monitor { get; set; }

        /// <summary>
        /// 获取或设置异常内容
        /// </summary>
        public MonitorException Exception { get; set; }


        /// <summary>
        /// 转换为通知提示标题
        /// </summary>
        /// <returns></returns>
        public string ToTitle()
        {
            return $"[{this.Monitor.Alias}]监控提醒";
        }

        /// <summary>
        /// 转换为通知提示内容
        /// </summary>
        /// <returns></returns>
        public string ToMessage()
        {
            var shortMessage = $"检测到[{this.Monitor.Alias}]于{DateTime.Now.ToString()}生产异常：{this.Exception.Message}";
            var builder = new StringBuilder()
                .AppendLine(shortMessage).AppendLine()
                .AppendLine("异常详细信息：")
                .AppendLine(this.Exception.ToString()).AppendLine();

            var inner = this.Exception.InnerException;
            while (inner != null)
            {
                builder.AppendLine("内部异常信息：")
                    .AppendLine(inner.ToString()).AppendLine();

                inner = inner.InnerException;
            }
            return builder.ToString();
        }
    }
}
