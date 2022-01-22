using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tasty.Models;
using Tasty.Models.ViewModels;
using Tasty.Models.Interfaceses;

namespace Tasty.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private SessionCart sessionCart;
        private IShopRepository shopRepository;

        public CartSummaryViewComponent(SessionCart serviceCart, IShopRepository repo)
        {
            sessionCart = serviceCart;
            shopRepository = repo;
        }

        public IViewComponentResult Invoke(int shopId)
        {
            Shop shop = shopRepository.Shops
                .FirstOrDefault(p => p.ShopId == shopId);

            return View(sessionCart.GetCart(shop));
        }
    }
}
