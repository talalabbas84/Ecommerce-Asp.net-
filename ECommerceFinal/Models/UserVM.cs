using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceFinal.Models
{
    public class UserVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}