using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models.Interfaceses
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private ApplicationDbContext context;

        public EFCategoryRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Category> Categories => context.Categories;

        public void SaveCategory(Category category)
        {
            if (category.CategoryId == 0)
            {
                context.Categories.Add(category);
            }
            else
            {
                Category dbEntry = context.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);
                if (dbEntry != null)
                {
                    dbEntry.Name = category.Name;
                }
            }
            context.SaveChanges();
        }

        public Category DeleteCategory(int categoryId)
        {
            Category dbEntry = context.Categories
                .FirstOrDefault(c => c.CategoryId == categoryId);
            if(dbEntry != null)
            {
                context.Categories.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

    }
}
