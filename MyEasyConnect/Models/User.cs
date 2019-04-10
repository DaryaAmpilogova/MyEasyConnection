using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEasyConnect.Models
{
    public class User
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surnames { get; set; }
        public string Profecion { get; set; }
        public string Avatar { get; set; }
        public int Points { get; set; }

    }
}