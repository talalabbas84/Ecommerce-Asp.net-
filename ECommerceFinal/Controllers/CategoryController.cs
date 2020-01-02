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
    public class CategoryController : Controller
    {
        CategoryService categoryService = new CategoryService();
        ImageService imageService = new ImageService();

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
        public ActionResult Create(CategoryVM model)
        {
            
            CategoryService categoryService = new CategoryService();

            Category cat = new Category();
            cat.Name = model.Name;
            cat.Description = model.Description;

            Image img = new Image();

            img.Name = model.ImagePath;
            img.IsProduct = false;

            categoryService.SaveCategory(cat);

            var category = categoryService.GetCategories().ToList().Where(x => x.Name == model.Name).First();

            img.ItemID = category.ID; 
            imageService.SaveImage(img);

           
            return RedirectToAction("CategoryTable");
        }


        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var category = categoryService.GetCategory(ID);

            Image img;
            CategoryVM model = new CategoryVM();



            if ((imageService.GetImages(ID).Count > 0))
            {
                if(!imageService.GetImages(category.ID).First().IsProduct)
                {
                img = imageService.GetImages(category.ID).First();
                model.ImagePath = imageService.GetImages(ID).First().Name;
                model.ImageID = imageService.GetImages(ID).First().ID;
                }
            }
            else
            {
                model.ImagePath = "/Content/images/noImage.png";

            }
            
            model.Description = category.Description;
            model.ID = category.ID;
            model.Name = category.Name; 


            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(CategoryVM model)
        {

            Category category = new Category();
            category.Name = model.Name;
            category.Description = model.Description;
            category.ID = model.ID;

            Image img = new Image();
            img.ID = model.ImageID;
            img.ItemID = category.ID;
            img.IsProduct = false;
            img.Name = model.ImagePath; 


            categoryService.UpdateCategory(category);
            imageService.UpdateImage(img);

            return RedirectToAction("CategoryTable");
        }
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            categoryService.DeleteCategory(ID);
            imageService.DeleteImage(ID,false);

            return RedirectToAction("CategoryTable");
        }
    }
}