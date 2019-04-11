using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEasyConnect.Models
{
    public class Message
    {
        public int ID { set; get; }
        public string Subject { set; get; }
        public string Content { set; get; }
        public DateTime Date { set; get; }
        public User Sender { set; get; }
        public int State { set; get; }
    }
}