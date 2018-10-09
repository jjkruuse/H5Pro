using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H5pro.Models
{
    // 2.1 MessageHandler object
    public class MessageHandler
    {
        public int MessageID { get; set; }
        public int FromUser { get; set; }
        public int ToUser { get; set; }
        public string Sub { get; set; }
        public string TextMessage { get; set; }
        public string FromName { get; set; }
    }


}