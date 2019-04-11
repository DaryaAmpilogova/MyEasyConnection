using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEasyConnect.Models
{
    public class Reminder
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string ReminderDate { get; set; }

        public string Title { get; set; }

        public string Note { get; set; }

        public int User { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }
    }
}