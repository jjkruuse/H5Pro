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
            if(ModelState.IsValid)
            {
               

                db.Messages.InsertOnSubmit(message);
                db.Messages.Context.SubmitChanges();

                return View();
            }
            return View();
        }
    }
}