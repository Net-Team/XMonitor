using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteMonitor
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

        public void Add(IService service)
        {
            this.serviceList.Add(service);
        }

        public IEnumerator<IService> GetEnumerator()
        {
            return this.serviceList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
