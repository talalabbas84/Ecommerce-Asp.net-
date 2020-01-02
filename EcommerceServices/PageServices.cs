using EcommerceDatabase;
using EcommerceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceServices
{
    public class PageServices
    {
        public List<Pages> GetPages()
        {
            using (var context = new Db())
            {

                return context.Pages.ToList();


            }
        }
        public void SavePage(Pages page)
        {
            using (var context = new Db())
            {


                context.Pages.Add(page);
                context.SaveChanges();

            }
        }
        public Pages GetPage(int ID)
        {
            using (var context = new Db())
            {
                return context.Pages.Find(ID);

            }
        }
        public void UpdatePage(Pages pages)
        {
            using (var context = new Db())
            {

                context.Entry(pages).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();



            }
        }
        public void DeletePage(int ID)
        {
            using (var context = new Db())
            {
                // context.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                var page = context.Pages.Find(ID);
                if (page.Slug != "home")
                {
                    context.Pages.Remove(page);
                    context.SaveChanges();
                }




            }
        }
    }
}
