using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewBag.Message = "Message from home controller";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Message from home controller";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Message from home controller";

            return View();
        }

    }
}
