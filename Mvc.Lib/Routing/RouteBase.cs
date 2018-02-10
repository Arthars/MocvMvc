using System.Web;

namespace Mvc.Lib
{
    public abstract class RouteBase
    {
        public abstract RouteData GetRouteData(HttpContextBase httpContextBase);
    }
}