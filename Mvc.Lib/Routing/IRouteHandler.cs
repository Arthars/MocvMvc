using System.Web;

namespace Mvc.Lib
{
    public interface IRouteHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext">当前请求的上下文</param>
        /// <returns></returns>
        IHttpHandler GetHttpHandler(RequestContext requestContext);
    }
}