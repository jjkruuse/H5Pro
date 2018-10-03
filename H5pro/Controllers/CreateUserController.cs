using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;

namespace H5pro.Controllers
{
    public class CreateUserController : Controller
    {
        // GET: 
        
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(user.PasswordHash, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            user.PasswordHash = savedPasswordHash;

            string salted = Convert.ToBase64String(salt);
            user.Salt = salted;
            if(ModelState.IsValid)
            {
                DataClassDataContext db = new DataClassDataContext();
                db.Users.InsertOnSubmit(user);
                db.Users.Context.SubmitChanges();
                return RedirectToAction("Start", "Start");
            }
            return View();
        }
    }
}