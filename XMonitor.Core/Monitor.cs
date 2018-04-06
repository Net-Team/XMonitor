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
    public abstract class Monitor<TOptions> : IMonitor, IDisposable where TOptions : IMonitorOptions
    {

#if NET45
        /// <summary>
        /// 完成的任务
        /// </summary>
        /// <returns></returns>
        private static readonly Task CompletedTask = Task.FromResult<object>(null);
#else
        /// <summary>
        /// 完成的任务
        /// </summary>
        /// <returns></returns>
        private static readonly Task CompletedTask = Task.CompletedTask;
#endif

        /// <summary>
        /// 定时器
        /// </summary>
        private readonly Timer timer;

        /// <summary>
        /// 获取别名
        /// </summary>
        public string Alias { get; }

        /// <summary>
        /// 获取监控目标标识
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// 获取任务选项
        /// </summary>
        public TOptions Options { get; }

        /// <summary>
        /// 构造监控对象
        /// </summary>
        /// <param name="options">任务选项</param>
        /// <param name="alias">对象别名</param>
        /// <param name="value">监控目标标识</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Monitor(TOptions options, string alias, object value)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrEmpty(alias))
                throw new ArgumentNullException(nameof(alias));

            value = value ?? throw new ArgumentNullException(nameof(value));

            this.Alias = alias;
            this.Value = value;
            this.Options = options;
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
                    this.timer.Change((Int64)this.Options.Interval.TotalMilliseconds, Timeout.Infinite);
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
        protected abstract Task OnCheckMonitorAsync();

        /// <summary>
        /// 执行监控
        /// 异常输出
        /// </summary>
        /// <param name="ex">异常消息</param>
        protected virtual Task OnCheckExceptionAsync(Exception ex)
        {
            this.Options.Logger.Error(ex);
            return CompletedTask;
        }

        /// <summary>
        /// 监控通知异常
        /// </summary>
        /// <param name="ex">异常消息</param>
        /// <returns></returns>
        protected virtual async Task NotifyAsync(Exception ex)
        {
            var context = new NotifyContext
            {
                Monitor = this,
                Exception = ex
            };

            foreach (var channel in this.Options.NotifyChannels)
            {
                try
                {
                    await channel?.NotifyAsync(context);
                }
                catch (Exception channelEx)
                {
                    this.Options.Logger?.Error(channelEx);
                }
            }
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
