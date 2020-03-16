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
    public class WidgetController : Controller
    {

        ProductService prodservice = new ProductService();
        ProductWidgetViewModel model = new ProductWidgetViewModel();
        ImageService imageservice = new ImageService();
        CategoryService categoryService = new CategoryService();
        string catID = "";
        // GET: Widget
        public ActionResult Products(bool isLatestProducts)
        {

            if (isLatestProducts)
            {

                model.Products = prodservice.GetLatestProducts(8);



                model.Images = imageservice.GetImages();
                model.isLatestProduct = true;

            }
            else
            {
                if (!(Session["catID"] == null))
                {
                    catID = Session["catID"].ToString();
                }


                if (String.IsNullOrEmpty(catID))
                {
                    model.Products = prodservice.GetProductsHome(8);
                }
                else
                {
                    model.Products = prodservice.Get8categoryProducts(Convert.ToInt32(catID));

                    catID = "";
                }
                model.Images = imageservice.GetImages();
                model.isLatestProduct = false;
            }

            return PartialView(model);

        }

        [HttpPost]
        public ActionResult Products(int? categoryID)
        {
            if (categoryID != null)
            {




                return RedirectToAction("Products");

            }
            return PartialView();


        }

        [HttpPost]
        public void CategoryDisplay(int id)
        {
            Session["catID"] = id.ToString();

          

        }


        [HttpGet]

        public ActionResult CategoryDisplay()
        {

            model.Products = prodservice.GetLatestProducts(8);

            model.Images = imageservice.GetImages();
            return PartialView(model);
        }
    }

}



