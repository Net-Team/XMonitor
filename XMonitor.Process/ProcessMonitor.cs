using System.Diagnostics;
using System.IO;
using XMonitor.Core;
using System.Linq;
using System.Threading.Tasks;

namespace XMonitor.Process
{
    /// <summary>
    /// 表示进程对象
    /// </summary>
    public class ProcessMonitor : Monitor<ProcessOptions>
    {
        /// <summary>
        /// 获取应用程序信息
        /// </summary>
        public ProcessInfo ProcessInfo { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alias">程应用程序别名</param>
        /// <param name="processInfo"></param>
        /// <param name="options"></param>
        /// 
        public ProcessMonitor(string alias, ProcessInfo processInfo, ProcessOptions options)
            : base(options, alias, processInfo)
        {

        }


        /// <summary>
        /// 查找对应的进程
        /// </summary>
        /// <returns></returns>
        public System.Diagnostics.Process FindProcess()
        {
            var processName = Path.GetFileNameWithoutExtension(this.FilePath);
            return System.Diagnostics.Process.GetProcessesByName(processName)?.FirstOrDefault();
        }

        protected override Task OnCheckMonitorAsync()
        {
            try
            {
                var process = monitor.FindProcess();
                if (process == null)
                {
                    process = System.Diagnostics.Process.Start(monitor.ToProcessStartInfo());
                }
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                await this.NotifyAsync(monitor, ex);
                this.options.Logger?.Error(ex);
            }
        }
    }
}
