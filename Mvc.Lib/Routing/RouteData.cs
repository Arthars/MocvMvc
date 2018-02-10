using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Lib
{
    public class RouteData
    {
        /// <summary>
        /// 从地址解析出来的变量列表
        /// </summary>
        public IDictionary<string, object> Values { get; private set; }

        /// <summary>
        /// 具有其他来源的变量列表
        /// </summary>
        public IDictionary<string, object> DataTokens { get; private set; }

        /// <summary>
        /// 用于返回真正的用于处理HTTP请求的httpHandler对象
        /// </summary>
        public IRouteHandler RouteHandler { get; set; }

        public RouteBase RouteBase { get; set; }

        public RouteData()
        {
            this.Values = new Dictionary<string, object>();
            this.DataTokens = new Dictionary<string, object>();
            this.DataTokens.Add("namespace", new List<string>());
        }

        public string Controller
        {
            get
            {
                object controllerName = string.Empty;
                this.Values.TryGetValue("controller", out controllerName);
                return controllerName.ToString();
            }
        }

        public string ActionName
        {
            get
            {
                object actionName = string.Empty;
                this.Values.TryGetValue("action", out actionName);
                return actionName.ToString();
            }
        }
    }
}