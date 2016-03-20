using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Example.Web.Core.Web;

namespace Example.Web.Areas.Admin.Controllers
{
    public class ToolController : AdminController
    {
       
        public ActionResult FormBuild()
        {
            return View();
        }
    }
}