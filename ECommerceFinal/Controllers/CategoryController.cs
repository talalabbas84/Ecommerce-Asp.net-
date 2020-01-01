using EcommerceEntities;
using EcommerceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceFinal.Controllers
{
    public class CategoryController : Controller
    {
        CategoryService categoryService = new CategoryService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

       

        public ActionResult CategoryTable(string search)
        {
            var category = categoryService.GetCategories();
            if (string.IsNullOrEmpty(search) == false)
            {
                category = category.Where(c => c.Name != null && c.Name.ToLower().Contains(search.ToLower())).ToList();
            }


            return PartialView(category);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }


        [HttpPost]
        public ActionResult Create(Category category)
        {

            CategoryService categoryService = new CategoryService();
            categoryService.SaveCategory(category);

           
            return RedirectToAction("CategoryTable");
        }


        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var category = categoryService.GetCategory(ID);
            return PartialView(category);
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            categoryService.UpdateCategory(category);
            return RedirectToAction("CategoryTable");
        }
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            categoryService.DeleteCategory(ID);
            return RedirectToAction("CategoryTable");
        }
    }
}