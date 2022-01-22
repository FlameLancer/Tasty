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
    public class CheckoutViewComponent : ViewComponent
    {
        private IShopRepository repository;

        public CheckoutViewComponent(IShopRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke(int shopId)
        {
            Shop shop = repository.Shops
                .Include(s => s.OpeningTimes)
                .FirstOrDefault(p => p.ShopId == shopId);

            return View(OrderController.NowEnd(shop));
        }
    }
}
