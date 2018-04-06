using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiClient;
using WebApiClient.Attributes;
using WebApiClient.Parameterables;

namespace XMonitor.Web
{
    /// <summary>
    /// 定义Http状态接口
    /// </summary>
    public interface IHttpStatusApi : IDisposable
    {
        /// <summary>
        /// 检测指定URL
        /// </summary>
        /// <param name="url">站点Uri</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        [HttpGet]
        ITask<HttpResponseMessage> CheckAsync([Url] Uri url, Timeout timeout);
    }
}
