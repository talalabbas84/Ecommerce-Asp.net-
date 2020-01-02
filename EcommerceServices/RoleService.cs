using EcommerceDatabase;
using EcommerceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceServices
{
    public class RoleService
    {

        public List<UserRoles> GetRoles()
        {
            using(var context = new Db())
            {
               return context.Roles.ToList();
            }
        }


        public UserRoles GetRoles(int ID)
        {
            using (var context = new Db())
            {
                return context.Roles.Where(x => x.UserID == ID).First();
            }
        }



    }
}
