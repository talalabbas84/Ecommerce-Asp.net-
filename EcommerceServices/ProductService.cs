﻿using EcommerceDatabase;
using EcommerceEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace EcommerceServices
{
    public class ProductService
    {
        public void SaveProduct(Product product)
        {
            using (var context = new Db())
            {
                context.Entry(product.Category).State = System.Data.Entity.EntityState.Unchanged;

                context.Products.Add(product);
                context.SaveChanges();

            }
        }

        public List<Product> GetProducts()
        {
            using (var context = new Db())
            {

                return context.Products.Include(x=> x.Category).ToList();

            }
        }
        public Product GetProducts(int ID)
        {
            using (var context = new Db())
            {
                return context.Products.Include(x=> x.Category).SingleOrDefault(x => x.ID == ID);

            }
        }
        public void UpdateProducts(Product product)
        {
            using (var context = new Db())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();



            }
        }
        public void DeleteProduct(int ID)
        {
            using (var context = new Db())
            {
                // context.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                var product = context.Products.Find(ID);
                context.Products.Remove(product);
                context.SaveChanges();



            }
        }
        public List<Product> GetLatestProducts(int numberOfProducts)
        {
            using (var context = new Db())
            {

                return context.Products.OrderByDescending(y => y.ID).Take(numberOfProducts).Include(x => x.Category).ToList();

            }
        }
        public List<Product> GetProductsHome(int num)
        {
            using (var context = new Db())
            {

                return context.Products.Take(num).Include(x => x.Category).ToList();

            }
        }
        public List<Product> Get8categoryProducts(int catID)
        {
            using (var context = new Db())
            {

                return context.Products.Take(8).Include(x => x.Category).Where(x => x.Category.ID == catID).ToList();

            }
        }
        public List<Product> GetProductsbyCategory(int ID)
        {
            using (var context = new Db())
            {
                return context.Products.Include(x => x.Category).Where(x => x.Category.ID == ID).ToList();

            }
        }


    }
}
