using EcommerceEntities;
using ECommerceFinal.Models;
using EcommerceServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceFinal.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        ProductService productService = new ProductService();
        CategoryService categoryService = new CategoryService();
        ImageService imageService = new ImageService();


        // GET: Product
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ProductTable(string search)
        {
            var products = productService.GetProducts();
            if (string.IsNullOrEmpty(search) == false)
            {
                products = products.Where(p => p.Name != null && p.Name.ToLower().Contains(search.ToLower())).ToList();
            }


            return PartialView(products);
        }


        [HttpGet]
        public ActionResult Create()
        {
            var categories = categoryService.GetCategories();
            return PartialView(categories);
        }

        [HttpPost]
        public ActionResult Create(ProductVM model)
        {
           
           var category  = categoryService.GetCategory(model.CategoryID);

            var product = new Product();
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Category = category;
            
            productService.SaveProduct(product);


            var newProducts = productService.GetProducts();

            Product newProd = newProducts.Where(x => x.Name == model.Name).First();

            if (!string.IsNullOrEmpty(model.ImagePath)){
                Image img = new Image();
                
                img.Name = model.ImagePath;
                img.ItemID = newProd.ID;
                img.IsProduct = true; 
                imageService.SaveImage(img);
            }



            return RedirectToAction("ProductTable");
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var product = productService.GetProducts(ID);

            var AllProd = productService.GetProducts();
            Product prodINeed = AllProd.Where(x => x.ID == ID).SingleOrDefault(); 




            ProductVM model = new ProductVM();

            model.ID = product.ID;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.CategoryID = product.Category.ID;

            var obj = (imageService.GetImages(ID).Count > 0);

            if ((imageService.GetImages(ID).Count > 0))
            {
                if (imageService.GetImages(ID).First().IsProduct)
                {

                    model.ImagePath = imageService.GetImages(ID).First().Name;
                    model.ImageID = imageService.GetImages(ID).First().ID;
                }
            }

            else
            {
                model.ImagePath = "/Content/images/noImage.png";

            }
         
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(ProductVM model)
        {
            Product product = new Product();

            product.ID = model.ID;
            product.Description = model.Description; ;
            product.Name = model.Name;
            product.Price = model.Price;
            product.Category = categoryService.GetCategory( model.CategoryID) ;

            Image img = new Image();
            img.Name = model.ImagePath;
            img.ID = model.ImageID;
            img.ItemID = model.ID;
            img.IsProduct = true;

            

            productService.UpdateProducts(product);
            if (!img.Name.Equals("./Content/images/noImage.png"))
            {

               


                string fullPath = Request.MapPath("./Content/uploads" + img.Name);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

            }
            

            return RedirectToAction("ProductTable");
        }
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            productService.DeleteProduct(ID);
            imageService.DeleteImage(ID, true);


            return RedirectToAction("ProductTable");
        }


        
        


    }



}