using EcommerceEntities;
using ECommerceFinal.Models.Page;
using EcommerceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceFinal.Controllers
{
    public class PagesController : Controller
    {
        PageServices pageServices = new PageServices();
        SidebarServices sidebarServices = new SidebarServices();
        PageVM model = new PageVM();
        // GET: Pages
        public ActionResult Index()
        {


            List<Pages> pageList;
            pageList = pageServices.GetPages().OrderBy(x => x.Sorting).ToList();
            return View(pageList);

        }
        [HttpGet]
        public ActionResult AddPage()
        {
            return View();
        }
         [HttpPost]
        public ActionResult AddPage(PageVM model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Pages pages = new Pages();
            pages.Title = model.Title;
            string slug;
            if (string.IsNullOrWhiteSpace(model.Slug))
            {
                slug = model.Title.Replace(" ", "-").ToLower();

            }
            else
            {
                slug = model.Slug.Replace(" ", "-").ToLower();
            }
            if (pageServices.GetPages().Any(x => x.Title == model.Title || pageServices.GetPages().Any(y => y.Slug == model.Slug)))
            {
                ModelState.AddModelError("", "That title or slug already exists");
            }
            pages.Slug = slug;
            pages.Body = model.Body;
            pages.HasSidebar = model.HasSidebar;
            pages.Sorting = 100;
            pageServices.SavePage(pages);
            TempData["SM"] = "You  have added a new page";

            return RedirectToAction("AddPage");
        }
        [HttpGet]
        public ActionResult EditPage(int id)

        {
            PageVM model;
            Pages dto = pageServices.GetPage(id);
            if (dto == null)
            {
                return Content("The page doesn't exist");
            }

            model = new PageVM(dto);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditPage(PageVM model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Pages pages = new Pages();
            int ID = model.ID;
            pages.ID = model.ID;
            pages.Title = model.Title;

            string slug = "home";
            if (model.Slug != "home")
            {
                if (string.IsNullOrWhiteSpace(model.Slug))
                {
                    slug = model.Title.Replace(" ", "-").ToLower();

                }
                else
                {
                    slug = model.Slug.Replace(" ", "-").ToLower();
                }
            }

            if (pageServices.GetPages().Where(x => x.ID != ID).Any(x => x.Title == model.Title || pageServices.GetPages().Where(y => y.ID != ID).Any(y => y.Slug == model.Slug)))
            {
                ModelState.AddModelError("", "That title or slug already exists");
            }
            pages.Slug = slug;
            pages.Body = model.Body;
            pages.HasSidebar = model.HasSidebar;


            pageServices.UpdatePage(pages);
            TempData["SM"] = "You  have edited a page";

            return RedirectToAction("Index");
        }

        public ActionResult PageDelete(int id)
        {
            Pages page = pageServices.GetPage(id);
            if (page == null)
            {
                return Content("This page doesn't exist");
            }
            else
            {
                pageServices.DeletePage(id);
            }
            return RedirectToAction("Index");

        }

        public ActionResult PageDetails(int id)
        {
            Pages page = pageServices.GetPage(id);
            if (page == null)
            {
                return Content("This page doesn't exist");
            }
            model = new PageVM(page);
            return View(model);

        }
        [HttpPost]
        public void ReorderPages(int[] id)
        {
            int count = 1;
            Pages page;
            foreach (var pageId in id)
            {
                page = pageServices.GetPage(pageId);
                page.Sorting = count;
                pageServices.UpdatePage(page);
                count++;
            }

        }

        [HttpGet]
        public ActionResult EditSidebar()
        {
            SidebBarVM model;

            Sidebar sidebar = sidebarServices.GetSidebar();
            model = new SidebBarVM(sidebar);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditSidebar(SidebBarVM model)
        {


            Sidebar sidebar = sidebarServices.GetSidebar();
            sidebar.Body = model.Body;

            sidebarServices.UpdateSidebar(sidebar);

            return RedirectToAction("EditSidebar");
        }

    }
}