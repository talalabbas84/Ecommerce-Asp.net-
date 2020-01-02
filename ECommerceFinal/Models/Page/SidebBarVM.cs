using EcommerceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceFinal.Models.Page
{
    public class SidebBarVM
    {
        public SidebBarVM()
        {

        }
        
        public SidebBarVM(Sidebar sidebar)
        {
            this.ID = sidebar.ID;
            this.Body = sidebar.Body;

        }
        public int ID { get; set; }
        [AllowHtml]
        public string Body { get; set; }
    }
}