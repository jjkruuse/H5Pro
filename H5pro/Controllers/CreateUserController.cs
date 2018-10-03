using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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