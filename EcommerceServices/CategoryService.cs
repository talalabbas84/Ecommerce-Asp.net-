using EcommerceDatabase;
using EcommerceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceServices
{
   public class CategoryService
    {

        public void SaveCategory(Category category)
        {
            using (var context = new Db())
            {
                context.Categories.Add(category);
                context.SaveChanges();

            }
        }

        public List<Category> GetCategories()
        {
            using (var context = new Db())
            {
                return context.Categories.ToList();

            }
        }
        public Category GetCategory(int ID)
        {
            using (var context = new Db())
            {
                return context.Categories.Find(ID);

            }
        }
        public void UpdateCategory(Category category)
        {
            using (var context = new Db())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();



            }
        }
        public void DeleteCategory(int ID)
        {
            using (var context = new Db())
            {
                // context.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                var category = context.Categories.Find(ID);
                context.Categories.Remove(category);
                context.SaveChanges();



            }
        }
    }
}
