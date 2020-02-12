using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRBAC.Models
{
    public class News
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime AddTime { get; set; }
        public Int64 OrderId { get; set; }
    }
}