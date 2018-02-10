using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mvc.Lib
{
    public interface IActionInvoker
    {
        void InvokeAction(ControllerContext context, string actionName);
    }
}
