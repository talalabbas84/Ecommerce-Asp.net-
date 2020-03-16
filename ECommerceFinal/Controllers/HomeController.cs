using ECommerceFinal.Models;
using EcommerceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceFinal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CategoryService categoryService = new CategoryService();
            ImageService imageService = new ImageService();
            var categories = categoryService.GetCategories().ToList();

            var category1 = categories[categories.Count - 1];
            var category2 = categories[categories.Count - 2];
            var category3 = categories[categories.Count - 3];


            var Images = imageService.GetImages().Where(x=> x.ItemID == category1.ID || x.ItemID == category2.ID || x.ItemID == category3.ID && x.IsProduct == false  ).ToList();






            return View(Images);
        }

    }
}