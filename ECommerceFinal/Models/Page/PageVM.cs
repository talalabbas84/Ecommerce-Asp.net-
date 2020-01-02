using EcommerceEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceFinal.Models.Page
{
    public class PageVM
    {
        public PageVM()
        {

        }
        public PageVM(Pages row)
        {
            ID = row.ID;
            Title = row.Title;
            Slug = row.Slug;
            Body = row.Body;
            Sorting = row.Sorting;
            HasSidebar = row.HasSidebar;

        }
        public int ID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Slug { get; set; }
        [AllowHtml]
        public string Body { get; set; }
        public int Sorting { get; set; }
        public bool HasSidebar { get; set; }

    }
}