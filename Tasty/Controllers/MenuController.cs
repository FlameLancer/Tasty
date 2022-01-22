using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tasty.Models;
using Tasty.Models.Interfaceses;
using Tasty.Models.ViewModels;

namespace Tasty.Controllers
{
    public class MenuController : Controller
    {
        private IShopRepository repository;
        
        public MenuController(IShopRepository repo)
        {
            repository = repo;
        }

        public IActionResult List(int shopId, string returnUrl)
        {
            
            Shop shop = repository.Shops
                .Include(s => s.Categories)
                .Include(s => s.OpeningTimes)
                .Include(s => s.MenuCategories.Where(mc => mc.isActive))
                    .ThenInclude(mc => mc.Items.Where(i => i.isActive))
                .FirstOrDefault(s => s.ShopId == shopId );
            if (!shop.isActive)
                return Redirect("/");
            DayOfWeek thisDay = DateTime.Now.DayOfWeek;
            DayOfWeek previousDay = DateTime.Now.AddDays(-1).DayOfWeek;
            TimeSpan now = DateTime.Now.TimeOfDay;
            if (now.Minutes > 30)
                now = TimeSpan.FromHours(now.Hours) + TimeSpan.FromHours(1) + TimeSpan.FromMinutes(30);
            else
                now = TimeSpan.FromHours(now.Hours) + TimeSpan.FromHours(1);
            bool isOpen = shop.OpeningTimes.Any(o => o.DayOfWeek == thisDay && (now >= o.Opening + TimeSpan.FromHours(1) && now <= o.Closing) ||
                                             o.DayOfWeek == previousDay && now + TimeSpan.FromDays(1) <= o.Closing);
            return View(new MenuViewModel { ReturnUrl = returnUrl, Shop = shop, IsOpen = isOpen });
        }

        public IActionResult Details(int shopId, string returnUrl)
        {
            Shop shop = repository.Shops.Where(s => s.ShopId == shopId)
                .Include(s => s.Categories)
                .Include(s => s.OpeningTimes)
                .Include(s => s.ShopAddress)
                .FirstOrDefault();
            DayOfWeek thisDay = DateTime.Now.DayOfWeek;
            DayOfWeek previousDay = DateTime.Now.AddDays(-1).DayOfWeek;
            TimeSpan now = DateTime.Now.TimeOfDay;
            if (now.Minutes > 30)
                now = TimeSpan.FromHours(now.Hours) + TimeSpan.FromHours(1) + TimeSpan.FromMinutes(30);
            else
                now = TimeSpan.FromHours(now.Hours) + TimeSpan.FromHours(1);
            bool isOpen = shop.OpeningTimes.Any(o => o.DayOfWeek == thisDay && (now >= o.Opening + TimeSpan.FromHours(1) && now <= o.Closing) ||
                                             o.DayOfWeek == previousDay && now + TimeSpan.FromDays(1) <= o.Closing);
            return View(new MenuViewModel { ReturnUrl = returnUrl, Shop = shop, IsOpen = isOpen });
        }
    }
}
