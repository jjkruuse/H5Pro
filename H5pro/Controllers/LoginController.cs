using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace H5pro.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User objUser)
        {
            if (ModelState.IsValid)
            {
                using (DataClassDataContext db = new DataClassDataContext())
                {
                    var obj = db.Users.Where(a => a.Username.Equals(objUser.Username) && a.PasswordHash.Equals(objUser.PasswordHash)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.UserID.ToString();
                        Session["UserName"] = obj.Username.ToString();
                        return RedirectToAction("Profil","User");
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Start", "Start");
        }
    }
}