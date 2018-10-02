using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H5pro.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Profil()
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

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Movies()
        {
            return View();
        }
    }
}