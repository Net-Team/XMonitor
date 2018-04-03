using XMonitor.Core;

namespace XMonitor.Process
{
    /// <summary>
    /// 表示进程对象
    /// </summary>
    public class ProcessMonitor : IMonitor
    {
        /// <summary>
        /// 获取或设置别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// 获取或设置进程的文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 取或设置启动参数字符串
        /// </summary>
        public string Arguments { get; set; }

        /// <summary>
        /// 取或设置工作路径
        /// </summary>
        public string WorkingDirectory { get; set; }

        /// <summary>
        /// 获取进程的文件路径
        /// </summary>
        object IMonitor.Value
        {
            get
            {
                return this.FilePath;
            }
        }
    }
}
