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

        public ActionResult Show()
        {
            DataClassDataContext db = new DataClassDataContext();
            List<Show> show = db.Shows.ToList();


            return View(show);
        }
        public ActionResult Createshow()
        {
            return View();
        }
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