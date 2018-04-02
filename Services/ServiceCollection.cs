using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// 表示服务集合
    /// </summary>
    public class ServiceCollection : IEnumerable<IService>
    {
        /// <summary>
        /// 服务集合
        /// </summary>
        private readonly List<IService> serviceList = new List<IService>();

        /// <summary>
        /// 获取服务的数量
        /// </summary>
        public int Count => this.serviceList.Count;

        /// <summary>
        /// 获取服务是否在运行
        /// </summary>
        public bool IsServicesRunning { get; private set; }

        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="service"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Add(IService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }
            this.serviceList.Add(service);
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
        public IEnumerator<IService> GetEnumerator()
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
