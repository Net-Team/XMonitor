using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XMonitor.Core
{
    /// <summary>
    /// 任务间隔执行
    /// </summary>
    /// <typeparam name="TMonitor">监控对象</typeparam>
    public abstract class MonitorChecker<TMonitor> : IDisposable where TMonitor : IMonitor
    {
        /// <summary>
        /// 定时器
        /// </summary>
        private readonly Timer timer;

        /// <summary>
        /// 执行间隔时间
        /// </summary>
        public TimeSpan Delay { get; private set; }

        /// <summary>
        /// 监控对象
        /// </summary>
        public TMonitor Monitor { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="monitor">监控对象</param>
        /// <param name="delay">间隔时间</param>
        public MonitorChecker(TMonitor monitor, TimeSpan delay)
        {
            this.Delay = delay;
            this.Monitor = monitor;
            this.timer = new Timer(async (state) =>
            {
                try
                {
                    await this.OnCheckMonitorAsync();
                }
                catch (Exception ex)
                {
                    await this.OnCheckExceptionAsync(ex);
                }
                finally
                {
                    this.timer.Change(delay.Milliseconds, Timeout.Infinite);
                }
            }, null, 0, Timeout.Infinite);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            this.timer.Dispose();
        }

        /// <summary>
        /// 执行监控
        /// </summary>
        /// <returns></returns>
        protected abstract Task OnCheckMonitorAsync();

        /// <summary>
        /// 执行监控
        /// 异常输出
        /// </summary>
        /// <param name="ex">异常消息</param>
        protected abstract Task OnCheckExceptionAsync(Exception ex);
    }
}
