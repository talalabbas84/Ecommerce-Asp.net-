using EcommerceDatabase;
using EcommerceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceServices
{
    public class SidebarServices
    {

        public Sidebar GetSidebar()
        {
            using (var context = new Db())
            {
                return context.Sidebars.Find(1);

            }
        }

        public void UpdateSidebar(Sidebar sidebar)
        {
            using (var context = new Db())
            {

                context.Entry(sidebar).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();



            }
        }
    }
}
