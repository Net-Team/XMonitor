using System;
using System.Collections;
using System.Collections.Generic;

namespace XMonitor.Core
{
    /// <summary>
    /// 表示监控对象集合
    /// </summary>
    public class MonitorCollection : IEnumerable<IMonitor>
    {
        /// <summary>
        /// 监控对象集合
        /// </summary>
        private readonly List<IMonitor> monitorList = new List<IMonitor>();

        /// <summary>
        /// 获取监控对象的数量
        /// </summary>
        public int Count => this.monitorList.Count;

        /// <summary>
        /// 获取监控是否在运行
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// 添加监控对象
        /// </summary>
        /// <param name="monitor">监控对象</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Add(IMonitor monitor)
        {
            if (monitor == null)
            {
                throw new ArgumentNullException(nameof(monitor));
            }
            this.monitorList.Add(monitor);
        }

        /// <summary>
        /// 启动所有监控对象
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void Start()
        {
            if (this.IsRunning == true)
            {
                throw new InvalidOperationException("监控对象已启动过..");
            }

            foreach (var item in this)
            {
                item.Start();
            }
        }

        /// <summary>
        /// 停止所有监控对象
        /// <exception cref="InvalidOperationException"></exception>
        /// </summary>
        public void Stop()
        {
            if (this.IsRunning == false)
            {
                throw new InvalidOperationException("监控对象已停止过..");
            }

            foreach (var item in this)
            {
                item.Stop();
            }
        }

        /// <summary>
        /// 返回迭代器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IMonitor> GetEnumerator()
        {
            return this.monitorList.GetEnumerator();
        }

        /// <summary>
        /// 返回迭代器
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
