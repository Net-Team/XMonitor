using System.Diagnostics;
using System.IO;
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

        /// <summary>
        /// 返回进程的名称
        /// </summary>
        /// <returns></returns>
        public string GetProcessName()
        {
            return Path.GetFileNameWithoutExtension(this.FilePath);
        }

        /// <summary>
        /// 转换为ProcessStartInfo对象
        /// </summary>
        /// <returns></returns>
        public ProcessStartInfo ToProcessStartInfo()
        {
            return new ProcessStartInfo
            {
                Arguments = this.Arguments,
                FileName = this.FilePath,
                WorkingDirectory = this.WorkingDirectory
            };
        }
    }
}
