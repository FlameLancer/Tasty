using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasty.Models;
using Tasty.Models.Interfaceses;
using Tasty.Models.ViewModels;

namespace Tasty.Controllers
{
    
    public class OrderController : Controller
    {
        private IShopRepository shopRepository;
        private IOrderRepository orderRepository;
        private SessionCart sessionCart;
        private UserManager<AppUser> userManager;

        public OrderController(UserManager<AppUser> usrMgr, IShopRepository repo, IOrderRepository repoService, SessionCart cartService)
        {
            shopRepository = repo;
            orderRepository = repoService;
            sessionCart = cartService;
            userManager = usrMgr;
        }
        public IActionResult Checkout(int shopId)
        {
            if (!shopRepository.Shops.FirstOrDefault(s => s.ShopId == shopId).isActive)
                return Redirect("/");
            ViewBag.ShopId = shopId;
            if (User.Identity.IsAuthenticated)
            {
                AppUser appUser = userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                return View(new Order
                {
                    City = appUser.City,
                    Zip = appUser.Zip,
                    Street = appUser.Street,
                    Phone = appUser.PhoneNumber,
                    Name = appUser.Name
                });
            }
            else
            {
                return View(new Order());
            }
            
        }

        public static CheckoutViewModel NowEnd(Shop shop)
        {
            int tday = (int)DateTime.Now.DayOfWeek;
            int yday = (int)DateTime.Now.AddDays(-1).DayOfWeek;
            int nday = (int)DateTime.Now.AddDays(1).DayOfWeek;
            var oday = TimeSpan.FromDays(1);
            var ohour = TimeSpan.FromHours(1);
            TimeSpan now = DateTime.Now.TimeOfDay;
            if (now.Minutes > 30)
                now = TimeSpan.FromHours(now.Hours) + ohour + TimeSpan.FromMinutes(30);
            else
                now = TimeSpan.FromHours(now.Hours) + ohour;
            TimeSpan start = shop.OpeningTimes.ElementAt(tday).Opening;
            TimeSpan end = shop.OpeningTimes.ElementAt(tday).Closing;
            TimeSpan yend = shop.OpeningTimes.ElementAt(yday).Closing;
            TimeSpan nstart = shop.OpeningTimes.ElementAt(nday).Opening;
            if ((now < start + TimeSpan.FromHours(1) || now > end) && now + oday > yend)
                return new CheckoutViewModel { End = TimeSpan.FromHours(0), Now = ohour };
            if (end.Days > 0 && end - oday == nstart)
            {
                end = now + oday;
            }
            else if (now + oday < yend && !(yend - oday == start))
            {
                end = yend - oday;
            }
            return new CheckoutViewModel { End = end, Now = now };
        }

        [HttpPost]
        public IActionResult Checkout(Order order, int shopId)
        {
            Shop shop = shopRepository.Shops
                .Include(s => s.OpeningTimes)
                .FirstOrDefault(s => s.ShopId == shopId);
            order.Shop = shop;
            order.CreateDate = DateTime.Now;
            order.DeliveryStatus = 0;
            Cart cart = sessionCart.GetCart(order.Shop);
            DayOfWeek thisDay = DateTime.Now.DayOfWeek;
            DayOfWeek previousDay = DateTime.Now.AddDays(-1).DayOfWeek;
            TimeSpan now = DateTime.Now.TimeOfDay;
            if (now.Minutes > 30)
                now = TimeSpan.FromHours(now.Hours) + TimeSpan.FromHours(1) + TimeSpan.FromMinutes(30);
            else
                now = TimeSpan.FromHours(now.Hours) + TimeSpan.FromHours(1);
            bool isOpen = shop.OpeningTimes.Any(o => o.DayOfWeek == thisDay && (now >= o.Opening + TimeSpan.FromHours(1) && now <= o.Closing) ||
                                             o.DayOfWeek == previousDay && now + TimeSpan.FromDays(1) <= o.Closing);
            
            var nowEnd = NowEnd(shop);
            bool isPossible = nowEnd.Now <= order.DeliveryTime && order.DeliveryTime <= nowEnd.End ;

            if (order.DeliveryTime.Days > 0)
                order.DeliveryTime -= TimeSpan.FromDays(1);
            
            if(order.Zip != null)
                if(!shop.PostCodes.Contains(order.Zip))
                    ModelState.AddModelError("", "Restauracja nie obsługuje podanego rejonu");

            if (!isPossible)
                ModelState.AddModelError("DeliveryTime", "Podano nieprawidłową godzine dostawy");

            if (!isOpen)
                ModelState.AddModelError("", "Zamówienie z tej restauracji zostały wstrzymane!");

            if (cart.Lines.Count() == 0)
                ModelState.AddModelError("", "Koszyk jest pusty!");

            if (cart.CheckWithMinPrice() != 0)
            {
                var s = "Do minimalnej wartości zamówienia brakuje: ";
                s += cart.CheckWithMinPrice().ToString("c");
                ModelState.AddModelError("", s);
            }

            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                orderRepository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                ViewBag.ShopId = shopId;
                return View(order);
            }
        }

        public ViewResult Completed()
        {
            sessionCart.Clear();
            return View();
        }   
    }
}
