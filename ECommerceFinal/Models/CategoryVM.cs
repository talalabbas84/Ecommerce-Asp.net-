using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerceFinal.Models
{
    public class CategoryVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int ImageID { get; set; }
    }
}