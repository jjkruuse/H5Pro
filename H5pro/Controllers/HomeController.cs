using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H5pro.Controllers
{
    public class HomeController : Controller
    {
        // 1.2.1 Checks if the user is logged in an responds accordingly
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Start", "Start");
            }
        }
    }
}