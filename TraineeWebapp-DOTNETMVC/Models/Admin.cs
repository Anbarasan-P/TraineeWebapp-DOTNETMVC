using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraineeWebapp_DOTNETMVC.Models
{
    public class Admin
    {
        public int AdminID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}