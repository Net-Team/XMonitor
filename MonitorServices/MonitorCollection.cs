using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorServices
{
    /// <summary>
    /// 表示监控的目标集合
    /// </summary>
    public class MonitorCollection<TValue> : IEnumerable<IMonitor<TValue>>
    {
        /// <summary>
        /// 监控的目标列表
        /// </summary>
        private readonly List<Monitor> monitors = new List<Monitor>();

        /// <summary>
        /// 监控目标的数量
        /// </summary>
        public int Count => this.monitors.Count;

        /// <summary>
        /// 添加监控目标
        /// </summary>
        /// <param name="alias">别名</param>
        /// <param name="value">值</param>
        public void Add(string alias, TValue value)
        {
            var monitor = new Monitor
            {
                Alias = alias,
                Value = value
            };
            this.monitors.Add(monitor);
        }

        /// <summary>
        /// 返回监控目标迭代器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IMonitor<TValue>> GetEnumerator()
        {
            return this.monitors.GetEnumerator();
        }

        /// <summary>
        /// 返回监控目标迭代器
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// 监控目标实现
        /// </summary>
        private class Monitor : IMonitor<TValue>
        {
            /// <summary>
            /// 获取或设置值
            /// </summary>
            public TValue Value { get; set; }

            /// <summary>
            /// 获取或设置别名
            /// </summary>
            public string Alias { get; set; }

            /// <summary>
            /// 获取或设置值
            /// </summary>
            object IMonitor.Value
            {
                get
                {
                    return this.Value;
                }
                set
                {
                    this.Value = (TValue)value;
                }
            }

            /// <summary>
            /// 转换为字符串
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return this.Alias;
            }
        }
    }
}