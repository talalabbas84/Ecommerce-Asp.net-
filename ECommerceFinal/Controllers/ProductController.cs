using EcommerceEntities;
using ECommerceFinal.Models;
using EcommerceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceFinal.Controllers
{
    public class ProductController : Controller
    {
        ProductService productService = new ProductService();


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
            CategoryService categoryService = new CategoryService();
            var categories = categoryService.GetCategories();
            return PartialView(categories);
        }

        [HttpPost]
        public ActionResult Create(ProductVM model)
        {
           CategoryService categoryService = new CategoryService();

           var category  = categoryService.GetCategory(model.CategoryID);

            var product = new Product();
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Category = category;

            productService.SaveProduct(product);

            return RedirectToAction("ProductTable");
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var product = productService.GetProducts(ID);
            return PartialView(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            productService.UpdateProducts(product);
            return RedirectToAction("ProductTable");
        }
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            productService.DeleteProduct(ID);
            return RedirectToAction("ProductTable");
        }

    }
}