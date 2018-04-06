using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMonitor.Core
{
    /// <summary>
    /// 表示监控集合
    /// </summary>
    public class MonitorCollection : IEnumerable<IMonitor>
    {
        /// <summary>
        /// 监控集合
        /// </summary>
        private readonly List<IMonitor> serviceList = new List<IMonitor>();

        /// <summary>
        /// 获取监控的数量
        /// </summary>
        public int Count => this.serviceList.Count;

        /// <summary>
        /// 获取监控是否在运行
        /// </summary>
        public bool IsServicesRunning { get; private set; }

        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="monitor">监控对象</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Add(IMonitor monitor)
        {
            if (monitor == null)
            {
                throw new ArgumentNullException(nameof(monitor));
            }
            this.serviceList.Add(monitor);
        }

        /// <summary>
        /// 启动所有服务
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void Start()
        {
            if (this.IsServicesRunning == true)
            {
                throw new InvalidOperationException("服务已启动过..");
            }

            foreach (var item in this)
            {
                item.Start();
            }
        }

        /// <summary>
        /// 停止所有服务
        /// <exception cref="InvalidOperationException"></exception>
        /// </summary>
        public void Stop()
        {
            if (this.IsServicesRunning == false)
            {
                throw new InvalidOperationException("服务已停止过..");
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
            return this.serviceList.GetEnumerator();
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
