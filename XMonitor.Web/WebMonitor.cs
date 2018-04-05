﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMonitor.Core;

namespace XMonitor.Web
{
    /// <summary>
    /// 网站监控
    /// </summary>
    public class WebMonitor : IMonitor
    {
        /// <summary>
        /// 获取或设置别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// 获取或设置网址
        /// </summary>
        public Uri Uri { get; set; }

        /// <summary>
        /// 获取或设置网址
        /// </summary>
        object IMonitor.Value
        {
            get
            {
                return this.Uri;
            }
        }
    }
}