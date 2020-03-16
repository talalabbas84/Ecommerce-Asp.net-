using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceEntities
{
    public class OrderDetail
    {
        public int ID { get; set; }
        public string fullname { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string email { get; set; }
        public decimal total { get; set; }
    }
}
