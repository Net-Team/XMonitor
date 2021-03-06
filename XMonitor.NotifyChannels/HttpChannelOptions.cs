﻿using System;
using System.Collections.Generic;
using XMonitor.Core;

namespace XMonitor.NotifyChannels
{
    /// <summary>
    /// Http 消息通知选项
    /// </summary>
    public class HttpChannelOptions
    {
        /// <summary>
        /// 或取或设置接收者接口地址
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// 获取请求头集合
        /// </summary>
        public List<KeyValuePair<string, string>> Header { get; } = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// HttpContent参数 
        /// 或取或设置消息标题参数委托
        /// </summary>
        public Func<NotifyContext, KeyValuePair<string, string>> Title { get; set; } = ctx => new KeyValuePair<string, string>("Title", ctx.ToTitle());

        /// <summary>
        /// HttpContent参数 
        /// 或取或设置消息内容委托
        /// </summary>
        public Func<NotifyContext, KeyValuePair<string, string>> Message { get; set; } = ctx => new KeyValuePair<string, string>("Message", ctx.ToMessage());
    }
}
