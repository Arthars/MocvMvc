using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Lib
{
    public class RawContentResult : ActionResult
    {
        public string RawData { get; set; }
        public RawContentResult(string rawData)
        {
            RawData = rawData;
        }

        public override void ExecuteResult(ControllerContext controllerContext)
        {
            controllerContext.RequestContext.HttpContext.Response.Write(this.RawData);
        }
    }
}
