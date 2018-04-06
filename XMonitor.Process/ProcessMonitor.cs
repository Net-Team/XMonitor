using System.Diagnostics;
using System.IO;
using XMonitor.Core;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace XMonitor.Process
{
    /// <summary>
    /// 表示应用程序对象
    /// </summary>
    class ProcessMonitor : Monitor<ProcessOptions>
    {
        /// <summary>
        /// 获取应用程序信息
        /// </summary>
        public ProcessInfo ProcessInfo { get; }

        /// <summary>
        /// 构建应用程序对象
        /// </summary>
        /// <param name="options">应用程序监控选项</param>
        /// <param name="alias">程应用程序别名</param>
        /// <param name="processInfo">应用程序信息</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public ProcessMonitor(ProcessOptions options, string alias, ProcessInfo processInfo)
            : base(options, alias, processInfo = processInfo ?? throw new ArgumentNullException(nameof(processInfo)))
        {
            if (string.IsNullOrEmpty(processInfo.FilePath))
            {
                throw new ArgumentNullException(nameof(processInfo.FilePath));
            }

            if (!File.Exists(processInfo.FilePath))
            {
                throw new ArgumentException("文件不存在.", nameof(processInfo.FilePath));
            }
        }

        /// <summary>
        /// 查找对应的进程
        /// </summary>
        /// <returns></returns>
        private System.Diagnostics.Process FindProcess()
        {
            var processName = Path.GetFileNameWithoutExtension(this.ProcessInfo.FilePath);
            return System.Diagnostics.Process.GetProcessesByName(processName)?.FirstOrDefault();
        }

        /// <summary>
        /// 执行一次检查
        /// </summary>
        /// <returns></returns>
        protected override async Task OnCheckMonitorAsync()
        {
            try
            {
                var process = this.FindProcess();
                if (process == null)
                {
                    process = System.Diagnostics.Process.Start(this.ProcessInfo.ToProcessStartInfo());
                }
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                await base.NotifyAsync(ex);
                base.Options.Logger?.Error(ex);
            }
        }
    }
}
