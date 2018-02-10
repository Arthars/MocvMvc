using System.Web;

namespace Mvc.Lib
{
    public class MvcHandler : IHttpHandler
    {
        public RequestContext RequestContext { get; set; }

        public MvcHandler(RequestContext requestContext)
        {
            this.RequestContext = requestContext;
        }

        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            string controllerName = RequestContext.RouteData.Controller;
            IControllerFactory controllerFactory = ControllerBuilder.Current.GetControllerFactory();
            IController controller = controllerFactory.CreateController(RequestContext, controllerName);
            controller.Execute(RequestContext);
        }
    }
}
