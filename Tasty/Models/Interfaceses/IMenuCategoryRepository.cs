using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models.Interfaceses
{
    public interface IMenuCategoryRepository
    {
        IQueryable<MenuCategory> MenuCategories { get; }
        void SaveMenuCategory(MenuCategory menuCategory);
        MenuCategory DeleteMenuCategory(int menuCatgoryId);
    }
}
