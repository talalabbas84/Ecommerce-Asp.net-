using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceEntities
{
    public class Image
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int ItemID { get; set; }
        public bool IsProduct { get; set; } 
    }
}
