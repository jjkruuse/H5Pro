using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using H5pro.Models;

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
        public ActionResult CreateUser(UserHandler uh)
        {
            if (validate(uh))
            {
                User user = new User();
                byte[] salt;
                new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

                var pbkdf2 = new Rfc2898DeriveBytes(uh.password, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);

                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);

                string savedPasswordHash = Convert.ToBase64String(hashBytes);
                user.PasswordHash = savedPasswordHash;

                string salted = Convert.ToBase64String(salt);
                user.Salt = salted;
                user.Username = uh.username;
                user.Age = uh.age;
                user.Email = uh.email;
                user.Gender = uh.gender;

                if (ModelState.IsValid)
                {
                    DataClassDataContext db = new DataClassDataContext();
                    db.Users.InsertOnSubmit(user);
                    db.Users.Context.SubmitChanges();
                    return RedirectToAction("Start", "Start");
                }
            }
            return View();
        }

        private bool validate(UserHandler user)
        {
            if (user.password.Length < 7)
            {
                return false;
            }

            if (!user.password.Equals(user.passTest))
            {
                return false;
            }

            if (!user.password.Any(c => char.IsUpper(c)))
            {
                return false;
            }

            if (!user.password.Any(c => char.IsLower(c)))
            {
                return false;
            }

            if (!user.password.Any(c => char.IsNumber(c)))
            {
                return false;
            }

            if ((user.username.Length < 3) || (user.username.Length > 20))
            {
                return false;
            }

            if ((user.age < 18) || (user.age > 99))
            {
                return false;
            }

            if ((user.gender < 0) || (user.gender > 2))
            {
                return false;
            }

            return true;
        }
    }
}