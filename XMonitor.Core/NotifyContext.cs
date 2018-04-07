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
    }
}
