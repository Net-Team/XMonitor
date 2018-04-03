using XMonitor.Core;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMonitor.Process
{
    /// <summary>
    /// 表示进程状态检测服务
    /// </summary>
    class ProcessMonitorService : IMonitorService
    {
        /// <summary>
        /// 选项
        /// </summary>
        private readonly ProcessOptions options;
        
        /// <summary>
        /// 获取或设置是否在运行
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// 进程状态检测服务
        /// </summary>
        /// <param name="options">选项</param>
        public ProcessMonitorService(ProcessOptions options)
        {
            this.options = options;
        }

        /// <summary>
        /// 启动监控
        /// </summary>
        public async void Start()
        {
            this.IsRunning = true;
            await Task.Yield();
            await this.RunAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 运行检测
        /// </summary>
        /// <returns></returns>
        private async Task RunAsync()
        {
            while (this.IsRunning == true)
            {
                foreach (var monitor in this.options.Monitors)
                {
                    try
                    {                        
                    }
                    catch (Exception ex)
                    {
                        await this.NotifyAsync(monitor, ex);
                        this.options.Logger?.Error(ex);
                    }
                }
            }
        }        

        /// <summary>
        /// 通知异常
        /// </summary>
        /// <param name="monitor">产生异常的服务</param>
        /// <param name="exception">异常</param>
        /// <returns></returns>
        private async Task NotifyAsync(IMonitor monitor, Exception exception)
        {
            var context = new NotifyContext
            {
                Monitor = monitor,
                Exception = exception,
            };

            foreach (var channel in this.options.NotifyChannels)
            {
                try
                {
                    await channel?.NotifyAsync(context);
                }
                catch (Exception ex)
                {
                    this.options.Logger?.Error(ex);
                }
            }
        }


        /// <summary>
        /// 停止监控
        /// </summary>
        public void Stop()
        {
            this.IsRunning = false;
        }
    }
}
