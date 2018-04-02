using System;
using System.Net.Http;
using WebApiClient;
using WebApiClient.Attributes;
using WebApiClient.Parameterables;

namespace HttpStatusMonitor
{
    /// <summary>
    /// 定义Http状态接口
    /// </summary>
    public interface IHttpStatusApi : IDisposable
    {
        /// <summary>
        /// 检测指定URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        [HttpGet]
        ITask<HttpResponseMessage> CheckAsync([Url] Uri url, Timeout timeout);
    }
}
