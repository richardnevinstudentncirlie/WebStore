using System;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;
using System.Collections.Generic;

namespace WebStore.Domain.Concrete 
{

    public class EFProductRepository : IProductRepository 
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Product> Products 
        {
            get { return context.Products; }
        }

 
        public void SaveProduct(Product product) 
        {

            if (product.ProductID == 0) 
            {
                product.CreatedAt = DateTime.Now;
                product.UpdatedAt = DateTime.Now;
                context.Products.Add(product);
            } 
            else 
            {
                Product dbEntry = context.Products.Find(product.ProductID);
                if (dbEntry != null) 
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.Quantity = product.Quantity;
                    dbEntry.ImageData = product.ImageData;
                    dbEntry.ImageMimeType = product.ImageMimeType;
                    dbEntry.Special = product.Special;
                    dbEntry.Seller = product.Seller;
                    dbEntry.Buyer = product.Buyer;
                    dbEntry.UpdatedAt = DateTime.Now;
                }
            }
            context.SaveChanges();
        }

        public Product DeleteProduct(int productID) 
        {
            Product dbEntry = context.Products.Find(productID);
            if (dbEntry != null) 
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}