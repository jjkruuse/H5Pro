using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H5pro.Models
{
    // 2.3 SearchHandler object
    public class SearchHandler
    {
        public string userName { get; set; }
        public int minAge { get; set; }
        public int maxAge { get; set; }
        public bool male { get; set; }
        public bool female { get; set; }
        public bool other { get; set; }
    }
}