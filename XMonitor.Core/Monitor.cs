using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XMonitor.Core
{
    /// <summary>
    /// 监控对象
    /// </summary>
    public abstract class Monitor : IMonitor, IDisposable
    {
        /// <summary>
        /// 获取或设置别名
        /// </summary>
        public string Alias { get; }

        /// <summary>
        /// 获取或设置网址
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// 定时器
        /// </summary>
        private readonly Timer timer;

        /// <summary>
        /// 任务选项
        /// </summary>
        public IMonitorOptions options { get; private set; }

        /// <summary>
        /// 构造监控对象
        /// </summary>
        /// <param name="alias">对象别名</param>
        /// <param name="value">对象值</param>
        /// <param name="options">监控选项</param>
        public Monitor(string alias, object value, IMonitorOptions options)
        {
            this.Alias = alias;
            this.Value = value;
            this.options = options;

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
                    this.timer.Change((Int64)this.options.Interval.TotalMilliseconds, Timeout.Infinite);
                }
            }, null, Timeout.Infinite, Timeout.Infinite);
        }

        /// <summary>
        /// 开始监控
        /// </summary>
        public void Start()
        {
            this.timer.Change(0, Timeout.Infinite);
        }

        /// <summary>
        /// 停止监控
        /// </summary>
        public void Stop()
        {
            this.timer.Change(Timeout.Infinite, Timeout.Infinite);
        }



        /// <summary>
        /// 执行检测
        /// </summary>
        /// <returns></returns>
        public abstract Task OnCheckMonitorAsync();

        /// <summary>
        /// 执行监控
        /// 异常输出
        /// </summary>
        /// <param name="ex">异常消息</param>
        protected virtual async Task OnCheckExceptionAsync(Exception ex)
        {
            this.options.Logger.Error(ex);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            this.timer.Dispose();
        }
    }
}
