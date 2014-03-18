using System;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;
using System.Collections.Generic;

namespace WebStore.Domain.Concrete 
{

    public class EFCategoryRepository : ICategoryRepository 
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Category> Categories 
        {
            get { return context.Categories; }
        }

 
        public void SaveCategory(Category category) 
        {

            if (category.CategoryID == 0) 
            {

                category.CreatedAt = DateTime.Now;
                category.UpdatedAt = DateTime.Now;
                context.Categories.Add(category);
            } 
            else 
            {
                Category dbEntry = context.Categories.Find(category.CategoryID);
                if (dbEntry != null) 
                {
                    dbEntry.CategoryCode = category.CategoryCode;
                    dbEntry.Description = category.Description;
                    dbEntry.UpdatedAt = DateTime.Now;
                }
            }
            context.SaveChanges();
        }

        public Category DeleteCategory(int categoryID) 
        {
            Category dbEntry = context.Categories.Find(categoryID);
            if (dbEntry != null) 
            {
                context.Categories.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}