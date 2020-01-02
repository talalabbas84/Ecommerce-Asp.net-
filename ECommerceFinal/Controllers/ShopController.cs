using ECommerceFinal.Models;
using EcommerceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommerceEntities;
namespace ECommerceFinal.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop


        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            ImageService imageService = new ImageService();
            ProductService productService = new ProductService();

            List<ShopVM> model = new List<ShopVM>();
            ShopVM vm;


            var products = productService.GetProducts();

         foreach(var product in products)
            {
                vm = new ShopVM();
                vm.Name = product.Name;
                vm.Price = product.Price;
                vm.Description = product.Description;
                vm.ID = product.ID; 
                List<Image> imgs = imageService.GetImages(product.ID);
               vm.Images = imgs; 
              
                model.Add(vm);
            }


            return View(model);

        }
    }
}