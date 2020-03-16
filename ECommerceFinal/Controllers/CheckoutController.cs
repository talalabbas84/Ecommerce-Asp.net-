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

    [Authorize]

    public class CheckoutController : Controller
    {


        public ActionResult Index()
        {
            if (Request.Cookies["CartProducts"] != null)
            {
                var value = Request.Cookies["CartProducts"].Value;

                string[] productIds = value.Split('-');
                List<int> ids = new List<int>();
                if (productIds.Length > 0 && productIds[0].Length > 0)
                {

                    foreach (string item in productIds)
                    {
                        ids.Add(Convert.ToInt32(item));
                    }

                }
                else
                {
                    return Content("There are no items that have been selected and the shopping cart is empty");
                }
                ProductService productService = new ProductService();
                ImageService imageService = new ImageService();

                var allProducts = productService.GetProducts();

                List<ProductVM> model = new List<ProductVM>();

                ProductVM productVM;
                foreach (var id in ids)
                {
                    var products = allProducts.Where(x => x.ID == id).ToList();

                    foreach (var product in products)
                    {
                        productVM = new ProductVM();

                        productVM.CategoryID = product.Category.ID;
                        productVM.Description = product.Description;
                        productVM.ID = product.ID;
                        productVM.Name = product.Name;
                        productVM.Price = product.Price;


                        if ((imageService.GetImages(product.ID).Count > 0))
                        {
                            if (imageService.GetImages(product.ID).First().IsProduct)
                            {

                                productVM.ImagePath = imageService.GetImages(product.ID).First().Name;
                                productVM.ImageID = imageService.GetImages(product.ID).First().ID;
                            }
                        }

                        else
                        {
                            productVM.ImagePath = "/Content/images/noImage.png";

                        }

                        productVM.Quantity = 1;  // hardcoded for now 
                        model.Add(productVM);

                    }
                }

                return View(model);


            }
            else
            {

                return Content("No Products in Shoppping Cart");
            }

        }

        [HttpPost]
        public ActionResult Checkout(OrderDetail orderDetail)
        {


            OrderDetailService orderDetailService = new OrderDetailService();

            orderDetailService.SaveOrderDetail(orderDetail); 


            return RedirectToAction("Index", "Home");

        }
    }
}
