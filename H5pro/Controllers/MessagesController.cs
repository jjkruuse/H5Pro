using H5pro.Models;
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

        // 1.4.1 Lists all messages recieved
        public ActionResult Messages()
        {
            List<Message> messages = db.Messages.ToList();
            int len = messages.Count;
            List<MessageHandler> messageHandlers = new List<MessageHandler>();
            if (len > 0)
            {
                for (int i = 0; i < len; i++)
                {
                    Console.Out.WriteLine(i + " " + messages[i].MessageID.ToString());

                    MessageHandler mh = new MessageHandler();

                    mh.MessageID = messages[i].MessageID;
                    mh.FromUser = messages[i].FromUser;
                    mh.Sub = messages[i].Sub;
                    mh.TextMessage = messages[i].TextMessage;
                    mh.ToUser = messages[i].ToUser;

                    int FromID = messages[i].FromUser;
                    var obj = db.Users.Where(a => a.UserID.Equals(FromID)).FirstOrDefault();

                    mh.FromName = obj.Username;

                    messageHandlers.Add(mh);
                }
            }
            return View(messageHandlers);
        }

        // 1.4.2 Read message
        public ActionResult ReadMessage(int Id)
        {
            MessageHandler mh = new MessageHandler();
            int userID;
            
            if (int.TryParse(Session["UserID"].ToString(), out userID))
            {
                Message message = db.Messages.Where(m => m.MessageID.Equals(userID)).FirstOrDefault();
                if (message != null)
                {
                    if ((message.FromUser.Equals(userID)) || message.ToUser.Equals(userID))
                    {
                        mh.FromUser = message.FromUser;
                        mh.Sub = message.Sub;
                        mh.TextMessage = message.TextMessage;
                        mh.ToUser = message.ToUser;

                        int FromID = message.FromUser;
                        var obj = db.Users.Where(a => a.UserID.Equals(FromID)).FirstOrDefault();

                        mh.FromName = obj.Username;
                        return View(mh);
                    }
                }
            }

            return View();
        }

        // 1.4.3 Write message site
        public ActionResult NewMessage(int? id, string From, string Sub)
        {
            MessageHandler message = new MessageHandler();
            if (id != null)
            {
                message.Sub = "RE: " + Sub;
                message.FromName = From;
                ViewData["Replying"] = message;
                return View();
            }
            message.Sub = "";
            message.FromName = "";
            ViewData["Replying"] = message;
            return View();
        }

        // 1.4.4 Sending message
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
    }
}