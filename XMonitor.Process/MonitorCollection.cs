using System;
using System.IO;
using XMonitor.Core;

namespace XMonitor.Process
{
    /// <summary>
    /// 表示监控的进程集合
    /// </summary>
    public class MonitorCollection : MonitorCollection<ProcessMonitor>
    {
        /// <summary>
        /// 添加进程
        /// </summary>
        /// <param name="alias">别名</param>
        /// <param name="filePath">进程文件路径</param>
        /// <param name="arguments">启动参数</param>
        /// <param name="workingDirectory">工作目录</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        public void Add(string alias, string filePath, string arguments = null, string workingDirectory = null)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (File.Exists(filePath) == false)
            {
                throw new FileNotFoundException(nameof(filePath));
            }

            if (workingDirectory == null)
            {
                workingDirectory = Path.GetDirectoryName(filePath);
            }
            var monitor = new ProcessMonitor
            {
                Alias = alias,
                FilePath = filePath,
                Arguments = arguments,
                WorkingDirectory = workingDirectory
            };
            base.Add(monitor);
        }
    }
}