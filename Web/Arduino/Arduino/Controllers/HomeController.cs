using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO.Ports;
using Arduino;

namespace Arduino.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SetLed(bool pos)
        {
            if (pos)
            {
                MvcApplication.Serial.Write(new byte[1] { 1 }, 0, 1);
                MvcApplication.Led = true;
            }
            else
            {
                MvcApplication.Serial.Write(new byte[1] { 0 }, 0, 1);
                MvcApplication.Led = false;
            }
            return new EmptyResult();
        }
        public ActionResult GetLed()
        {
            string cp = MvcApplication.data; ; //current position
            return Json(new { pos = cp });
        }
    }
}