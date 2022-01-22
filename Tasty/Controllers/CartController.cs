using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasty.Models;
using Tasty.Models.Interfaceses;
using Tasty.Models.ViewModels;

namespace Tasty.Controllers
{
    public class CartController : Controller
    {
        private IItemRepository itemRepository;
        private IShopRepository shopRepository;
        private SessionCart sessionCart;

        public CartController(IItemRepository repo1, IShopRepository repo2, SessionCart serviceCart)
        {
            itemRepository = repo1;
            shopRepository = repo2;
            sessionCart = serviceCart;
        }

        public IActionResult Index(int shopId, string returnUrl)
        {
            Shop shop = shopRepository.Shops
                .Where(p => p.ShopId == shopId)
                .FirstOrDefault();

            if (!shop.isActive)
                return Redirect("/");

            return View(new CartIndexViewModel
            {
                Cart = sessionCart.GetCart(shop),
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int shopId, int itemId, string returnUrl)
        {
            Item item = itemRepository.Items
                .FirstOrDefault(p => p.ItemId == itemId);
            Shop shop = shopRepository.Shops
                .FirstOrDefault(p => p.ShopId == shopId);

            if (item != null)
            {
                Cart cart = sessionCart.GetCart(shop);
                cart.AddItem(item, 1);
                sessionCart.SaveCart(cart);
            }
            return RedirectToAction("Index", new { shopId, returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int shopId, int ItemId, string returnUrl)
        {
            Item item = itemRepository.Items
                .FirstOrDefault(p => p.ItemId == ItemId);
            Shop shop = shopRepository.Shops
                .FirstOrDefault(p => p.ShopId == shopId);

            if (item != null)
            {
                Cart cart = sessionCart.GetCart(shop);
                cart.RemoveLine(item);
                sessionCart.SaveCart(cart);
            }
            return RedirectToAction("Index", new { shopId, returnUrl });
        }
        public RedirectToActionResult ChangeQuantity(int shopId, int ItemId, string returnUrl, int difference)
        {
            Item item = itemRepository.Items
                .FirstOrDefault(p => p.ItemId == ItemId);
            Shop shop = shopRepository.Shops
                .FirstOrDefault(p => p.ShopId == shopId);

            if (item != null)
            {
                Cart cart = sessionCart.GetCart(shop);
                cart.ChangeQuantity(item, difference);
                sessionCart.SaveCart(cart);
            }
            return RedirectToAction("Index", new { shopId, returnUrl });
        }
    }
}
