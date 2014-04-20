using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.WebUI.Models;
using WebStore.WebUI.HtmlHelpers;

namespace WebStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult About()
        {
            bool userLoggedOn;
            int userId;
            
            userLoggedOn = UserSessionData.UserLoggedOn;
            userId = UserSessionData.UserId;

            AppSettings appSettings = new AppSettings();
            string appSettingKeyValue;
            appSettingKeyValue = appSettings.getAppSetting("customsetting1");

            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "";

            return View();
        }
    }
}
