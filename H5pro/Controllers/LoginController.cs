using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;

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
                    var obj = db.Users.Where(a => a.Username.Equals(objUser.Username)).FirstOrDefault();

                    if (obj != null)
                    {
                        Boolean auth = true;
                        string savedPassHash = obj.PasswordHash;
                        byte[] hashBytes = Convert.FromBase64String(savedPassHash);
                        byte[] salt = new byte[16];
                        Array.Copy(hashBytes, 0, salt, 0, 16);
                        var pbkdf2 = new Rfc2898DeriveBytes(objUser.PasswordHash, salt, 10000);
                        byte[] hash = pbkdf2.GetBytes(20);

                        for (int i = 0; i < 20; i++)
                        {
                            if (hashBytes[i+16] != hash[i])
                            {
                                auth = false;
                            }
                        }

                        if (auth == true)
                        {
                            Session["UserID"] = obj.UserID.ToString();
                            Session["UserName"] = obj.Username.ToString();
                            return RedirectToAction("Profil", "User");
                        }
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