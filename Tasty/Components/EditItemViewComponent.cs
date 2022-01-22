using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Tasty.Controllers;
using Tasty.Models;
using Tasty.Models.ViewModels;
using Tasty.Models.Interfaceses;

namespace Tasty.Components
{
    public class EditItemViewComponent : ViewComponent
    {
        private IMenuCategoryRepository repository;

        public EditItemViewComponent(IMenuCategoryRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke(int shopId)
        {
            IEnumerable<MenuCategory> menuCategories = repository.MenuCategories
                .Include(mc => mc.Shop)
                .Where(mc => mc.Shop.ShopId == shopId);
            return View(menuCategories);
        }
    }
}
