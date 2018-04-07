namespace XMonitor.Core
{
    /// <summary>
    /// 定义监控的对象
    /// </summary>
    public interface IMonitor
    {
        /// <summary>
        /// 获取别名
        /// </summary>
        string Alias { get; }

        /// <summary>
        /// 获取监控目标标识
        /// </summary>
        object Value { get; }

        /// <summary>
        /// 启动监控
        /// </summary>
        void Start();

        /// <summary>
        /// 停止监控
        /// </summary>
        void Stop();    
    }
}
