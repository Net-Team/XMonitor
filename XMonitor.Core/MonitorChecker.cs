using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XMonitor.Core
{
    /// <summary>
    /// 任务检测执行
    /// </summary>
    /// <typeparam name="TMonitor">监控对象</typeparam>
    public abstract class MonitorChecker<TMonitor> : IDisposable where TMonitor : IMonitor
    {
        /// <summary>
        /// 定时器
        /// </summary>
        private readonly Timer timer;

        /// <summary>
        /// 监控对象
        /// </summary>
        public TMonitor Monitor { get; private set; }

        /// <summary>
        /// 任务
        /// </summary>
        public IMonitorServiceOptions options { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="monitor">监控对象</param>
        /// <param name="options">监控服务选项</param>
        public MonitorChecker(TMonitor monitor, IMonitorServiceOptions options)
        {
            this.Monitor = monitor;
            this.options = options;
            this.timer = new Timer(async (state) =>
            {
                try
                {
                    await this.OnCheckMonitorAsync();
                }
                catch (Exception ex)
                {
                    this.OnCheckException(ex);
                }
                finally
                {
                    this.timer.Change((Int64)this.options.Interval.TotalMilliseconds, Timeout.Infinite);
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
        public abstract Task OnCheckMonitorAsync();

        /// <summary>
        /// 执行监控
        /// 异常输出
        /// </summary>
        /// <param name="ex">异常消息</param>
        public virtual void OnCheckException(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
