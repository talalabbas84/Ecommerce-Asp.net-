using EcommerceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceFinal.Models
{
    public class ProductWidgetViewModel
    {
        public List<Product > Products { get; set; }
        public List<Image> Images { get; set; }
        public string ImagePath { get; set; }
        public int ImageID { get; set; }
        public bool isLatestProduct { get; set; }
                                           // public List<int> ID { get; set; }

    }
    
}