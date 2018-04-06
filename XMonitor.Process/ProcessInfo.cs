using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMonitor.Process
{

    /// <summary>
    /// 应用程序信息
    /// </summary>
    public class ProcessInfo
    {
        /// <summary>
        /// 获取进程的文件路径
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// 获取启动参数字符串
        /// </summary>
        public string Arguments { get; }

        /// <summary>
        /// 获取工作路径
        /// </summary>
        public string WorkingDirectory { get; }

        /// <summary>
        /// 构建应用程序信息
        /// </summary>
        /// <param name="filePath">进程的文件路径</param>
        /// <param name="arguments">启动参数字符串</param>
        /// <param name="workingDirectory">工作路径</param>
        public ProcessInfo(string filePath, string arguments = null, string workingDirectory = null)
        {
            this.FilePath = filePath;
            this.Arguments = arguments;
            this.WorkingDirectory = workingDirectory;
        }

        /// <summary>
        /// 转换为程序启动信息
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
