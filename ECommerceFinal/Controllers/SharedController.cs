using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceFinal.Controllers
{
    public class SharedController : Controller
    {
        // GET: Shared
        public JsonResult UploadImage()
        {

            JsonResult jsonResult = new JsonResult();
            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var file = Request.Files[0];

                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/content/upload/"), fileName);

                file.SaveAs(path);

                jsonResult.Data = new { Success = true, ImageURL = string.Format("/Content/upload/{0}", fileName) };
                //var newImage = new Image() { Name = fileName };
                

            }catch(Exception ex)
            {
                jsonResult.Data = new { Success = false, Message = ex.Message };
            }

            return jsonResult;
        }
    }
}