using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mvc.Lib
{
    public class DefaultModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, string modelName, Type modelType)
        {
            if (modelType.IsValueType || typeof(string) == modelType)
            {
                object instance = GetValueTypeInstance(controllerContext, modelName, modelType);
                if (instance != null)
                {
                    return instance;
                }

                return Activator.CreateInstance(modelType);
            }

            object modelInstance = Activator.CreateInstance(modelType);
            foreach (var property in modelType.GetProperties())
            {
                if (property.CanWrite == false)
                {
                    continue;
                }

                if (property.PropertyType.IsValueType == false && property.PropertyType != typeof(string))
                {
                    continue;
                }

                object propertyValue = GetValueTypeInstance(controllerContext, property.Name, property.PropertyType);
                if (propertyValue != null)
                {
                    property.SetValue(modelInstance, propertyValue, null);
                }
            }

            return modelInstance;
        }

        private object GetValueTypeInstance(ControllerContext controllerContext, string modelName, Type modelType)
        {
            var form = HttpContext.Current.Request.Form;
            if (form != null && form.AllKeys.Contains(modelName))
            {
                return Convert.ChangeType(form[modelName], modelType);
            }

            if (controllerContext.RequestContext.RouteData.Values.Any(it => it.Key.Equals(modelName, StringComparison.OrdinalIgnoreCase)))
            {
                return Convert.ChangeType(controllerContext.RequestContext.RouteData.Values[modelName.ToLower()], modelType);
            }

            if (controllerContext.RequestContext.RouteData.DataTokens.Any(it => it.Key.Equals(modelName, StringComparison.OrdinalIgnoreCase)))
            {
                return Convert.ChangeType(controllerContext.RequestContext.RouteData.DataTokens[modelName], modelType);
            }

            return null;
        }
    }
}
