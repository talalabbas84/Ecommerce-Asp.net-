using EcommerceEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDatabase
{
   public class Db : DbContext
    {
        public Db() : base("DbConnection")
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoles> Roles { get; set; }
        public DbSet<Pages> Pages { get; set; }
        public DbSet<Sidebar> Sidebars { get; set; }


    }
}
