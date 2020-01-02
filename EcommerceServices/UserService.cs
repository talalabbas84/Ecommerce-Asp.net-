using EcommerceDatabase;
using EcommerceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceServices
{
    public class UserService
    {
        public void SaveUser(User user)
        {
            using (var context = new Db())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }



        public List<User> GetUsers( )
        {
            using (var context = new Db())
            {
                return  context.Users.ToList();
            }
        }


        public User GetUser(String Email)
        {
            using (var context = new Db())
            {
                return context.Users.Where(x=> x.Email == Email).First();
            }
        }


    }
}
