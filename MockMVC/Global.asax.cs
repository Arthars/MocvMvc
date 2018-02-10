using Mvc.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace MockMVC
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.Add(
                "WithPrefix",
                new Route
                {
                    Url = "prefix/{controller}/{action}"
                });

            RouteTable.Routes.Add(
                "Default", 
                new Route
                {
                    Url = "{controller}/{action}",
                    Defaults = new
                    {
                        controller = "Home",
                        action = "Index"
                    }
                });
            ControllerBuilder.Current.SetControllerFactory(new DefaultControllerFactory());
        }
    }
}