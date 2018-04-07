using System;
using System.IO;
using System.Threading.Tasks;
using XMonitor.Core;

namespace XMonitor.Process
{
    /// <summary>
    /// 表示进程监控对象
    /// </summary>
    class ProcessMonitor : Monitor<ProcessOptions>
    {
        /// <summary>
        /// 获取进程描述信息
        /// </summary>
        public ProcessInfo ProcessInfo { get; private set; }

        /// <summary>
        /// 进程监控对象
        /// </summary>
        /// <param name="options">进程监控选项</param>
        /// <param name="alias">进程别名</param>
        /// <param name="processInfo">进程信息</param>
        /// <exception cref="ArgumentNullException"></exception>      
        public ProcessMonitor(ProcessOptions options, string alias, ProcessInfo processInfo)
            : base(options, alias, processInfo ?? throw new ArgumentNullException(nameof(processInfo)))
        {
            this.ProcessInfo = processInfo;
        }

        /// <summary>
        /// 执行一次检查
        /// </summary>
        /// <returns></returns>
        protected override async Task OnCheckMonitorAsync()
        {
            try
            {
                if (this.ProcessInfo.IsRunning() == false)
                {
                    base.Options.Logger?.Debug("进程已经被停止，正在恢复.");
                    this.ProcessInfo.Start();
                    base.Options.Logger?.Debug("进程已经被停止，已恢复启动..");
                }
            }
            catch (FileNotFoundException ex)
            {
                var message = "进程文件已被删除..";
                base.Options.Logger?.Debug(message);
                await base.NotifyAsync(new MonitorException(message, ex));
            }
        }
    }
}
