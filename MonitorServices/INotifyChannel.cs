using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorServices
{
    /// <summary>
    /// 定义异常通知通道
    /// </summary>
    public interface INotifyChannel
    {
        /// <summary>
        /// 异常通知
        /// </summary>
        /// <param name="context">通知上下文</param>
        /// <returns></returns>
        Task NotifyAsync(NotifyContext context);
    }
}
