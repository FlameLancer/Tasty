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
    public class HomeController : Controller
    {
        private ICategoryRepository categoryRepository;
        private IShopRepository shopRepository;

        public HomeController(ICategoryRepository repo1, IShopRepository repo2)
        {
            categoryRepository = repo1;
            shopRepository = repo2;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ShopList(string postCode, string sortOrder, string searchString, string categoryName)
        {
            ViewBag.GetPostCode = postCode;
            ViewBag.SearchString = searchString;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name" : sortOrder;
            ViewBag.CategoryName = categoryName;

            string s = ViewBag.NameSortParam;

            IEnumerable<Shop> shops = shopRepository.Shops
                .Include(s => s.Categories)
                .Include(s => s.OpeningTimes);
            shops = shops.Where(s => s.PostCodes.Contains(postCode))
                         .Where(s => s.isActive);
            if (!String.IsNullOrEmpty(categoryName))
            {
                shops = shops.Where(s => s.Categories.Any(c => c.Name == categoryName));
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                shops = shops.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }
            /*
                Monday 4:33
                Sunday 16:00 - 1.06:00
                Monday 10:00 - 22:00
                thisDay = Monday
                previousDay = Sunday
                now = 5:30
                todayClosing = 22:00
                yesterdayClosing = 1.06:00
                openShops = if(o.DayOfWeek == thisDay && (now >= o.Opening + TimeSpan.FromHours(1) && now <= todayClosing) ||
                                 o.DayOfWeek == previousDay && now + TimeSpan.FromDays(1) <= yesterdayClosing)
                closedShops = shops - openShops
            */
            DayOfWeek thisDay = DateTime.Now.DayOfWeek;
            DayOfWeek previousDay = DateTime.Now.AddDays(-1).DayOfWeek;
            TimeSpan now = DateTime.Now.TimeOfDay;
            if (now.Minutes > 30)
                now = TimeSpan.FromHours(now.Hours) + TimeSpan.FromHours(1) + TimeSpan.FromMinutes(30);
            else
                now = TimeSpan.FromHours(now.Hours) + TimeSpan.FromHours(1);
            IEnumerable<Shop>openShops = shops.Where(
                s => s.OpeningTimes.Any(o => o.DayOfWeek == thisDay && (now >= o.Opening + TimeSpan.FromHours(1) && now <= o.Closing) ||
                                             o.DayOfWeek == previousDay && now + TimeSpan.FromDays(1) <= o.Closing));
            shops = shops.Except(openShops);

            switch (sortOrder)
            {
                case "nameDesc":
                    shops = shops.OrderByDescending(s => s.Name);
                    openShops = openShops.OrderByDescending(s => s.Name);
                    break;
                default:
                    shops = shops.OrderBy(s => s.Name);
                    openShops = openShops.OrderBy(s => s.Name);
                    break;
            }

            return View(new ShopListViewModel
            {
                Categories = categoryRepository.Categories,
                Code = postCode,
                ClosedShops = shops,
                Shops = openShops
            });
        }
    }
}
