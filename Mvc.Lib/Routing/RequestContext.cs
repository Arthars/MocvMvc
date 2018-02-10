using System.Web;

namespace Mvc.Lib
{
    public class RequestContext
    {
        public virtual HttpContextBase HttpContext { get; set; }

        public virtual RouteData RouteData { get; set; }
    }
}