using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Lib
{
    public class ControllerActionInvoker : IActionInvoker
    {
        public IModelBinder ModelBinder { get; set; }

        public ControllerActionInvoker()
        {
            ModelBinder = new DefaultModelBinder();
        }

        public void InvokeAction(ControllerContext context, string actionName)
        {
            MethodInfo method = context.Controller.GetType().GetMethods().FirstOrDefault(it => string.Equals(actionName, it.Name));
            List<object> parameters = new List<object>();
            foreach(var parameter in method.GetParameters())
            {
                var value = this.ModelBinder.BindModel(context, parameter.Name, parameter.ParameterType);
                parameters.Add(value);
            }

            var actionResult = method.Invoke(context.Controller, parameters.ToArray()) as ActionResult;
            actionResult.ExecuteResult(context);
        }
    }
}
