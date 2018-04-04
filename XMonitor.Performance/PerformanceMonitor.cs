using System;
using System.Diagnostics;
using XMonitor.Core;

namespace XMonitor.Performance
{
    /// <summary>
    /// 性能监控
    /// </summary>
    public class PerformanceMonitor : IMonitor
    {
        /// <summary>
        /// 获取或设置别名
        /// </summary>
        public string Alias { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public object Value => throw new NotImplementedException();
    }
}
