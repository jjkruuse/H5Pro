using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H5pro.Controllers
{
    public class MessagesController : Controller
    {
        DataClassDataContext db = new DataClassDataContext();

        // GET: Messages
        public ActionResult Messages()
        {
            List<Message> messages = db.Messages.ToList();

            return View(messages);
        }




        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message message)
        {

            var obj = db.Users.Where(a => a.Username.Equals(Request["SendTo"])).FirstOrDefault();
            if (obj != null)
            {
                message.ToUser = obj.UserID;
                int sender = int.Parse(Session["UserID"].ToString());
                message.FromUser = sender;
                if (ModelState.IsValid)
                {
                    db.Messages.InsertOnSubmit(message);
                    db.Messages.Context.SubmitChanges();

                    return View();
                }
            }
            else
            {
                // Insert code to tell that there is no such user.
            }

            return View();
        }

        public ActionResult Reply()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Reply()
        {
            var obj = db.Users.Where(a => a.Username.Equals(Request["SendTo"])).FirstOrDefault();
            if (obj != null)
            {
                message.ToUser = obj.UserID;
                int sender = int.Parse(Session["UserID"].ToString());
                message.FromUser = sender;
                if (ModelState.IsValid)
                {
                    db.Messages.InsertOnSubmit(message);
                    db.Messages.Context.SubmitChanges();

                    return View();
                }
            }
            return View();
        }
    }
}