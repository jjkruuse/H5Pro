using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H5pro.Controllers
{
    public class UserController : Controller
    {
        // 1.7.1 View profile
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

        // 1.7.2 Edit profile
        public ActionResult Edit()
        {
            return View();
        }

        // 1.7.3 View shows
        public ActionResult Show()
        {
            DataClassDataContext db = new DataClassDataContext();
            List<Show> show = db.Shows.ToList();


            return View(show);
        }

        // 1.7.4 Create show site
        public ActionResult Createshow()
        {
            return View();
        }

        // 1.7.5 Create show function
        [HttpPost]
        public ActionResult Createshow(Show show)
        {
            if (ModelState.IsValid)
            {
                DataClassDataContext db = new DataClassDataContext();
                db.Shows.InsertOnSubmit(show);
                db.Shows.Context.SubmitChanges();
                return View();
            }
            return View();
        }
    }
}