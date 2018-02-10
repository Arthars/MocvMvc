using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.Lib
{
    public class ControllerContext
    {
        public RequestContext RequestContext { get; set; }

        public ControllerBase Controller { get; set; }
    }
}
