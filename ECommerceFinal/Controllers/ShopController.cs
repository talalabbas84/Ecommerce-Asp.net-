using ECommerceFinal.Models;
using EcommerceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommerceEntities;
using PagedList;
using PagedList.Mvc;
namespace ECommerceFinal.Controllers
{
    public class ShopController : Controller
    {// GET: Shop
        CategoryService categoryService = new CategoryService();
        ProductService productService = new ProductService();
        ImageService imageService = new ImageService();
        List<ShopVM> model = new List<ShopVM>();
        ShopVM vm;
        string catID;
        string prodID;


        [HttpGet]
        public ActionResult Index()
        {
            ImageService imageService = new ImageService();

            List<Product> products;
            if (!(Session["catID"] == null))
            {
                catID = Session["catID"].ToString();
            }

            if (String.IsNullOrEmpty(catID))
            {
                products = productService.GetProducts();
            }
            else
            {
                products = productService.GetProductsbyCategory(Convert.ToInt32(catID));

                catID = "";
            }


            foreach (var product in products)
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
        [HttpPost]
        public void Index(int ID)
        {
            Session["prodID"] = ID.ToString();

        }
        [HttpGet]
        public ActionResult SideBarCategories()
        {
            var category = categoryService.GetCategories();
            foreach (var cat in category)
            {
                vm = new ShopVM();
                vm.category = cat;
                model.Add(vm);
            }



            return PartialView(model);
        }
        [HttpPost]
        public void SideBarCategories(int ID)
        {
            Session["catID"] = ID.ToString();
        }

        public ActionResult ProductDetails()
        {
            ShopVM model = new ShopVM();
            Product product = null;
            if (!(Session["prodID"] == null))
            {
                prodID = Session["prodID"].ToString();
            }
            if (!String.IsNullOrEmpty(prodID))
            {
                model.Product = productService.GetProducts(Convert.ToInt32(prodID));



            }

            var obj = (imageService.GetImages(Convert.ToInt32(prodID)).Count > 0);

            if ((imageService.GetImages(Convert.ToInt32(prodID)).Count > 0))
            {
                if (imageService.GetImages(Convert.ToInt32(prodID)).First().IsProduct)
                {

                    model.ImagePath = imageService.GetImages(Convert.ToInt32(prodID)).First().Name;
                    model.ImageID = imageService.GetImages(Convert.ToInt32(prodID)).First().ID;
                }
            }

            else
            {
                model.ImagePath = "/Content/images/noImage.png";

            }

            return View(model);
        }
    }
}