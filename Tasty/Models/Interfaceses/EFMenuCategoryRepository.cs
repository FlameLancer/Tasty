using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models.Interfaceses
{
    public class EFMenuCategoryRepository : IMenuCategoryRepository
    {
        private ApplicationDbContext context;

        public EFMenuCategoryRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<MenuCategory> MenuCategories => context.MenuCategories;

        public void SaveMenuCategory(MenuCategory menuCategory)
        {
            if(menuCategory.MenuCategoryId == 0)
            {
                context.MenuCategories.Add(menuCategory);
            }
            else
            {
                MenuCategory dbEntry = context.MenuCategories.FirstOrDefault(mc => mc.MenuCategoryId == menuCategory.MenuCategoryId);
                if(dbEntry != null)
                {
                    dbEntry.Items = menuCategory.Items;
                    dbEntry.Name = menuCategory.Name;
                    dbEntry.Priority = menuCategory.Priority;
                }
            }
            context.SaveChanges();
        }

        public MenuCategory DeleteMenuCategory(int menuCategoryId)
        {
            MenuCategory dbEntry = context.MenuCategories
                .FirstOrDefault(mc => mc.MenuCategoryId == menuCategoryId);
            if (dbEntry != null)
            {
                context.MenuCategories.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
