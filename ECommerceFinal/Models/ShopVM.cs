using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EcommerceEntities;

namespace ECommerceFinal.Models
{
    public class ShopVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<Image> Images { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
    }
}