using System.Collections.Generic;
using System.Web;
using System.Linq;
using Mvc.Lib.Helpers;
using System.Reflection;

namespace Mvc.Lib
{
    public class Route : RouteBase
    {
        public IRouteHandler RouteHandler { get; set; }

        /// <summary>
        /// Url模板： {controller}/{action}/{id}
        /// </summary>
        public string Url { get; set; }

        public IDictionary<string, object> DataTokens { get; set; }

        public object Defaults { get; set; }

        public Route()
        {
            this.DataTokens = new Dictionary<string, object>();
            this.RouteHandler = new MvcRouteHandler();
        }

        public override RouteData GetRouteData(HttpContextBase httpContextBase)
        {
            IDictionary<string, object> variables;
            if(this.Match(httpContextBase.Request.AppRelativeCurrentExecutionFilePath.Substring(2), out variables))
            {
                RouteData routeData = new RouteData();
                foreach(var item in variables)
                {
                    routeData.Values.Add(item.Key.ToLower(), item.Value);
                }

                foreach(var item in DataTokens)
                {
                    routeData.DataTokens.Add(item.Key.ToLower(), item.Value);
                }

                routeData.RouteHandler = this.RouteHandler;

                return routeData;
            }

            return null;
        }

        /// <summary>
        /// Reuqest url和template url的模板是否匹配
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        protected bool Match(string requestUrl, out IDictionary<string, object> variables)
        {
            variables = new Dictionary<string, object>();
            string[] requestVariables = requestUrl.Split('/');
            string[] templateUrlVariables = this.Url.Split('/');
            if(requestUrl == string.Empty && Defaults != null)
            {
                var properties = Defaults.GetType().GetProperties();
                if (properties.Length != templateUrlVariables.Length)
                { 
                    return false;
                }

                // Use default value
                foreach (PropertyInfo propertyInfo in properties)
                {
                    variables.Add(propertyInfo.Name, propertyInfo.GetValue(Defaults));
                }

                return true;
            }

            if(requestVariables.Length != templateUrlVariables.Length)
            {
                return false;
            }

            foreach(var variable in templateUrlVariables)
            {
                if(variable.StartsWith("{") && variable.EndsWith("}"))
                {
                    var index = templateUrlVariables.FindIndex(iterator => iterator == variable);
                    variables.Add(variable.Trim("{}".ToCharArray()), requestVariables[index]);
                }
            }

            return true;
        }
    }
}
