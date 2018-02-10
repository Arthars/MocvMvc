using Mvc.Lib;

namespace MockMVC
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index(SimpleModel simpleModel)
        {
            string content = string.Format("Controller: {0} <br/> Action: {1}", simpleModel.Controller, simpleModel.Action);
            return new RawContentResult(content);
        }
    }
}