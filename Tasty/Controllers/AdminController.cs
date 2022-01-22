using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasty.Models;
using Tasty.Models.Interfaceses;
using Tasty.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Tasty.Controllers
{
    [Authorize(Roles = "Administratorzy, Kurierzy, Kierownicy, Pracownicy")]
    public class AdminController : Controller
    {
        private IShopRepository shopRepository;
        private IOrderRepository orderRepository;
        private ICategoryRepository categoryRepository;
        private IMenuCategoryRepository menuCategoryRepository;
        private IItemRepository itemRepository;
        private IAuthorizationService authService;
        public int PageSize = 10;

        public AdminController(IShopRepository repo1, IOrderRepository repo2, IMenuCategoryRepository repo3, IItemRepository repo4, ICategoryRepository repo5, IAuthorizationService auth)
        {
            shopRepository = repo1;
            orderRepository = repo2;
            menuCategoryRepository = repo3;
            itemRepository = repo4;
            categoryRepository = repo5;
            authService = auth;
        }

        [Authorize(Roles = "Administratorzy, Kierownicy")]
        public async Task<RedirectToActionResult> ChangeShopStatus(int shopId, string isActive, int page)
        {
            Shop shop = shopRepository.Shops
                .Include(s => s.Users)
                .FirstOrDefault(o => o.ShopId == shopId);
            if (shop == null)
            {
                return RedirectToAction(nameof(Shops));
            }
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, shop, "DostępDoSklepów");
            if (authorized.Succeeded)
            {
                shop.isActive = !shop.isActive;
                shopRepository.ChangeShopStatus(shop);
                return RedirectToAction(nameof(Shops), new { isActive = isActive, page = page });
            }
            else
            {
                return RedirectToAction(nameof(Shops));
            }
        }

        public IActionResult Shops(string isActive, int page = 1)
        {
            ViewBag.IsActive = isActive;
            bool? isAct = null;
            IEnumerable<Shop> shops;
            if (User.IsInRole("Kierownicy") || User.IsInRole("Pracownicy"))
            {
                shops = shopRepository.Shops
                    .Include(s => s.Users)
                    .Include(s => s.Orders)
                    .Where(s => s.Users.Any(u => u.UserName.Equals(User.Identity.Name)))
                    .OrderBy(s => s.Name);
            }
            else
            {
                shops = shopRepository.Shops
                    .Include(s => s.Orders)
                    .OrderBy(s => s.Name);
            }

            if (isActive != null)
            {
                switch (isActive)
                {
                    case "Aktywne":
                        shops = shops.Where(o => o.isActive == true);
                        isAct = true;
                        break;
                    case "Nieaktywne":
                        shops = shops.Where(o => o.isActive == false);
                        isAct = false;
                        break;
                    default:
                        break;
                }
            }
            shops = shops
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            return View(new ShopsViewModel
            { 
                Shops = shops,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = isAct == null ?
                    shopRepository.Shops.Count() :
                    shopRepository.Shops.Where(o => o.isActive == isAct).Count()
                }
            });
        }

        [Authorize(Roles = "Administratorzy, Kierownicy, Pracownicy")]
        public async Task<IActionResult> Shop(int shopId)
        {

            Shop shop = shopRepository.Shops
                .Include(s => s.Orders)
                    .ThenInclude(o => o.Lines)
                .Include(s => s.Users)
                .Include(s => s.Categories)
                .Include(s => s.OpeningTimes)
                .Include(s => s.ShopAddress)
                .Include(s => s.MenuCategories.OrderBy(mc => mc.Priority))
                    .ThenInclude(mc => mc.Items)
                .FirstOrDefault(o => o.ShopId == shopId);
            if (shop == null)
            {
                return RedirectToAction(nameof(Shops));
            }
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, shop, "DostępDoSklepów");
            if (authorized.Succeeded)
            {
                return View(shop);
            }
            else
            {
                return new ChallengeResult();
            }
        }

        [Authorize(Roles = "Administratorzy")]
        public IActionResult Categories(int page = 1)
        {
            IEnumerable<Category> categories = categoryRepository.Categories
                .Include(c => c.Shops)
                .OrderByDescending(c => c.Shops.Count())
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            return View(new CategoriesViewModel
            {
                Categories = categories,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = categoryRepository.Categories.Count()
                }
            });
        }
        public RedirectToActionResult ChangeOrderStatus(int orderId, Order.Status? status, Order.Status statusChange, int page)
        {
            Order order = orderRepository.Orders
                .Include(o => o.Shop)
                .FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                return RedirectToAction(nameof(Orders));
            }
            order.DeliveryStatus = statusChange;
            orderRepository.SaveOrder(order);
            return RedirectToAction(nameof(Orders), new { shopId = order.Shop.ShopId, status = status, page = page });
        }

        public IActionResult Orders(int shopId, Order.Status? status, int page = 1)
        {
            ViewBag.ShopId = shopId;
            ViewBag.Status = status;
            IEnumerable<Order> orders = orderRepository.Orders
                .Where(o => o.Shop.ShopId == shopId)
                .OrderBy(o => o.CreateDate);
            if (status != null)
            {
                orders = orders.Where(o => o.DeliveryStatus == status);
            }
            orders = orders
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            return View(new OrdersViewModel
            {
                Orders = orders,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = status == null ?
                    orderRepository.Orders.Where(o => o.Shop.ShopId == shopId).Count() :
                    orderRepository.Orders.Where(o => o.Shop.ShopId == shopId && o.DeliveryStatus == status).Count()
                }
            });
        }

        public IActionResult Order(int orderId, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            Order order = orderRepository.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                return RedirectToAction(nameof(Orders));
            }
            return View(order);
        }

        [Authorize(Roles = "Administratorzy, Kierownicy")]
        public async Task<IActionResult> EditDetails(int shopId)
        {
            Shop shop = shopRepository.Shops
                .Include(s => s.ShopAddress)
                .Include(s => s.Users)
                .Include(s => s.OpeningTimes)
                .Include(s => s.Categories)
                .FirstOrDefault(s => s.ShopId == shopId);
            if (shop == null)
            {
                return RedirectToAction(nameof(Shops));
            }
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, shop, "DostępDoSklepów");
            if (authorized.Succeeded)
            {
                List<OpeningTime> openingTimes = shop.OpeningTimes.ToList();
                return View(new EditDetailsViewModel
                {
                    Shop = shop,
                    OpeningTimes = openingTimes,
                    Categories = new SelectList(categoryRepository.Categories, "CategoryId", "Name"),
                    SelectedCategories = shop.Categories.Select(c => c.CategoryId)
                });
            }
            else
            {
                return new ChallengeResult();
            }
            
        }
        [HttpPost]
        [Authorize(Roles = "Administratorzy, Kierownicy")]
        public async Task<IActionResult> EditDetails(EditDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                Shop shop = shopRepository.Shops
                    .Include(s => s.Users)
                    .FirstOrDefault(s => s.ShopId == model.Shop.ShopId);
                AuthorizationResult authorized = await authService.AuthorizeAsync(User, shop, "DostępDoSklepów");
                if (authorized.Succeeded)
                {
                    model.Shop.Categories = categoryRepository.Categories.Where(c => model.SelectedCategories.Contains(c.CategoryId)).ToList();
                    model.Shop.OpeningTimes = model.OpeningTimes;
                    shopRepository.SaveShop(model.Shop);
                    TempData["message"] = $"Zapisano {model.Shop.Name}.";
                    return RedirectToAction("Shop", new { shopId = model.Shop.ShopId });
                }
                else
                {
                    return new ChallengeResult();
                }
            }
            else
            {
                model.Categories = new SelectList(categoryRepository.Categories, "CategoryId", "Name");
                return View(model);
            }
        }

        [Authorize(Roles = "Administratorzy")]
        public IActionResult CreateShop()
        {
            Shop shop = new Shop
            {
                OpeningTimes = new List<OpeningTime>(),
                Categories = new List<Category>(),
                MenuCategories = new List<MenuCategory>(),
                ShopAddress = new ShopAddress(),
            };
            OpeningTime openingTime1 = new OpeningTime { DayOfWeek = DayOfWeek.Monday };
            OpeningTime openingTime2 = new OpeningTime { DayOfWeek = DayOfWeek.Tuesday };
            OpeningTime openingTime3 = new OpeningTime { DayOfWeek = DayOfWeek.Wednesday };
            OpeningTime openingTime4 = new OpeningTime { DayOfWeek = DayOfWeek.Thursday };
            OpeningTime openingTime5 = new OpeningTime { DayOfWeek = DayOfWeek.Friday };
            OpeningTime openingTime6 = new OpeningTime { DayOfWeek = DayOfWeek.Saturday };
            OpeningTime openingTime7 = new OpeningTime { DayOfWeek = DayOfWeek.Sunday };
            shop.OpeningTimes.Add(openingTime7, openingTime1, openingTime2, openingTime3, openingTime4, openingTime5, openingTime6);
            List<OpeningTime> openingTimes = shop.OpeningTimes.ToList();
            return View("EditDetails", new EditDetailsViewModel
            { 
                Shop = shop,
                OpeningTimes = openingTimes,
                Categories = new SelectList(categoryRepository.Categories, "CategoryId", "Name"),
                SelectedCategories = shop.Categories.Select(c => c.CategoryId)
            });
        }

        [Authorize(Roles = "Administratorzy")]
        public IActionResult DeleteShop(int shopId, int page)
        {
            Shop shop = shopRepository.DeleteShop(shopId);
            if (shop != null)
            {
                TempData["message"] = $"Usunięto {shop.Name}.";
            }
            return RedirectToAction("Shops", new { page = page });
        }

        [Authorize(Roles = "Administratorzy")]
        public IActionResult EditCategory(int categoryId)
        {
            Category category = categoryRepository.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (category == null)
            {
                return RedirectToAction(nameof(Categories));
            }
            return View();
        } 

        [HttpPost]
        [Authorize(Roles = "Administratorzy")]
        public IActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.SaveCategory(category);
                TempData["message"] = $"Zapisano {category.Name}.";
                return RedirectToAction("Categories");
            }
            else
            {
                return View(category);
            }
        }

        [Authorize(Roles = "Administratorzy")]
        public IActionResult CreateCategory() => View("EditCategory", new Category { Shops = new List<Shop>() });

        [HttpPost]
        [Authorize(Roles = "Administratorzy")]
        public IActionResult DeleteCategory(int categoryId, int page)
        {
            Category category = categoryRepository.DeleteCategory(categoryId);
            if(category != null)
            {
                TempData["message"] = $"Usunięto {category.Name}.";
            }
            return RedirectToAction("Categories", new { page = page });
        }

        [Authorize(Roles = "Administratorzy, Kierownicy")]
        public async Task<RedirectToActionResult> ChangeMenuCategoryStatus(int menuCategoryId, int shopId)
        {
            Shop shop = shopRepository.Shops
                .Include(s => s.Users)
                .FirstOrDefault(s => s.ShopId == shopId);
            if (shop == null)
            {
                return RedirectToAction(nameof(Shops));
            }
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, shop, "DostępDoSklepów");
            if (authorized.Succeeded)
            {
                MenuCategory menuCategory = menuCategoryRepository.MenuCategories
                .FirstOrDefault(mc => mc.MenuCategoryId == menuCategoryId);
                if (menuCategory == null)
                {
                    return RedirectToAction(nameof(Shop), shopId);
                }
                menuCategory.isActive = !menuCategory.isActive;
                menuCategoryRepository.SaveMenuCategory(menuCategory);
                return RedirectToAction(nameof(Shop), new { shopId = shopId });
            }
            else
            {
                return RedirectToAction(nameof(Shop), new { shopId = shopId });
            }
        }

        [Authorize(Roles = "Administratorzy, Kierownicy")]
        public async Task<IActionResult> EditMenuCategory(int menuCategoryId)
        {
            MenuCategory menuCategory = menuCategoryRepository.MenuCategories
               .Include(mc => mc.Shop)
               .FirstOrDefault(mc => mc.MenuCategoryId == menuCategoryId);
            Shop shop = shopRepository.Shops
                .Include(s => s.Users)
                .FirstOrDefault(s => s.ShopId == menuCategory.Shop.ShopId);
            if (shop == null)
            {
                return RedirectToAction(nameof(Shops));
            }
            if (menuCategory == null)
            {
                return RedirectToAction(nameof(Shop), shop.ShopId);
            }
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, shop, "DostępDoSklepów");
            if (authorized.Succeeded)
            {
                ViewBag.ShopId = menuCategory.Shop.ShopId;
                return View(menuCategory);
            }
            else
            {
                return new ChallengeResult();
            }
        }
            
        [HttpPost]
        [Authorize(Roles = "Administratorzy, Kierownicy")]
        public async Task<IActionResult> EditMenuCategory(MenuCategory menuCategory, int shopId)
        {
            Shop shop = shopRepository.Shops
                .Include(s => s.Users)
                .FirstOrDefault(i => i.ShopId == shopId);
            if (shop == null)
            {
                return RedirectToAction(nameof(Shops));
            }
            if (ModelState.IsValid)
            {
                AuthorizationResult authorized = await authService.AuthorizeAsync(User, shop, "DostępDoSklepów");
                if (authorized.Succeeded)
                {
                    menuCategory.Shop = shop;
                    menuCategoryRepository.SaveMenuCategory(menuCategory);
                    TempData["message"] = $"Zapisano {menuCategory.Name}.";
                    return RedirectToAction("Shop", new { shopId = shopId });
                }
                else
                {
                    return new ChallengeResult();
                }     
            }
            else
            {
                return View(menuCategory);
            }
        }

        [Authorize(Roles = "Administratorzy, Kierownicy")]
        public async Task<IActionResult> CreateMenuCategory(int shopId)
        {
            Shop shop = shopRepository.Shops
                .Include(s => s.Users)
                .FirstOrDefault(i => i.ShopId == shopId);
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, shop, "DostępDoSklepów");
            if (authorized.Succeeded)
            {
                ViewBag.ShopId = shopId;
                return View("EditMenuCategory", new MenuCategory());
            }
            else
            {
                return new ChallengeResult();
            }
        }

        [Authorize(Roles = "Administratorzy, Kierownicy")]
        public async Task<IActionResult> DeleteMenuCategory(int menuCategoryId, int shopId)
        {
            Shop shop = shopRepository.Shops
                .Include(s => s.Users)
                .FirstOrDefault(i => i.ShopId == shopId);
            if (shop == null)
            {
                return RedirectToAction(nameof(Shops));
            }
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, shop, "DostępDoSklepów");
            if (authorized.Succeeded)
            {
                MenuCategory menuCategory = menuCategoryRepository.DeleteMenuCategory(menuCategoryId);
                if (menuCategory != null)
                {
                    TempData["message"] = $"Usunięto {menuCategory.Name}.";
                }
                return RedirectToAction("Shop", new { shopId = shopId });
            }
            else
            {
                return new ChallengeResult();
            } 
        }

        [Authorize(Roles = "Administratorzy, Kierownicy")]
        public async Task<RedirectToActionResult> ChangeItemStatus(int itemId, int shopId)
        {
            Shop shop = shopRepository.Shops
                .Include(s => s.Users)
                .FirstOrDefault(s => s.ShopId == shopId);
            if (shop == null)
            {
                return RedirectToAction(nameof(Shops));
            }
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, shop, "DostępDoSklepów");
            if (authorized.Succeeded)
            {
                Item item = itemRepository.Items
                .FirstOrDefault(i => i.ItemId == itemId);
                if (item == null)
                {
                    return RedirectToAction(nameof(Shop), shopId);
                }
                item.isActive = !item.isActive;
                itemRepository.SaveItem(item);
                return RedirectToAction(nameof(Shop), new { shopId = shopId });
            }
            else
            {
                return RedirectToAction(nameof(Shop), new { shopId = shopId });
            }
        }

        [Authorize(Roles = "Administratorzy, Kierownicy")]
        public async Task<IActionResult> EditItem(int itemId, int shopId)
        {
            Shop shop = shopRepository.Shops
                .Include(s => s.Users)
                .FirstOrDefault(s => s.ShopId == shopId);
            if (shop == null)
            {
                return RedirectToAction(nameof(Shops));
            }
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, shop, "DostępDoSklepów");
            if (authorized.Succeeded)
            {
                Item item = itemRepository.Items
                .Include(i => i.MenuCategory)
                .FirstOrDefault(i => i.ItemId == itemId);
                ViewBag.ShopId = shopId;
                ViewBag.MenuCategoryId = item.MenuCategory.MenuCategoryId;
                return View(item);
            }
            else
            {
                return new ChallengeResult();
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administratorzy, Kierownicy")]
        public async Task<IActionResult> EditItem(Item item, int menuCategoryId, int shopId)
        {
            MenuCategory menuCategory = menuCategoryRepository.MenuCategories
                    .FirstOrDefault(mc => mc.MenuCategoryId == menuCategoryId);
            Shop shop = shopRepository.Shops
                .Include(s => s.Users)
                .FirstOrDefault(s => s.ShopId == shopId);
            if (shop == null)
            {
                return RedirectToAction(nameof(Shops));
            }
            if (menuCategory == null)
            {
                return RedirectToAction(nameof(Shop), shopId);
            }

            bool isPossible = menuCategoryRepository.MenuCategories.Any(mc => mc.MenuCategoryId == menuCategoryId);

            if (!isPossible)
                ModelState.AddModelError("", "Podano nieprawidłową kategorie");

            if (ModelState.IsValid)
            {
                AuthorizationResult authorized = await authService.AuthorizeAsync(User, shop, "DostępDoSklepów");
                if (authorized.Succeeded)
                {
                    item.MenuCategory = menuCategory;
                    itemRepository.SaveItem(item);
                    TempData["message"] = $"Zapisano {item.Name}.";
                    return RedirectToAction("Shop", new { shopId = shopId });
                }
                else
                {
                    return new ChallengeResult();
                }  
            }
            else
            {
                ViewBag.ShopId = shopId;
                ViewBag.MenuCategoryId = menuCategoryId;
                return View(item);
            }
        }

        [Authorize(Roles = "Administratorzy, Kierownicy")]
        public async Task<IActionResult> CreateItem(int menuCategoryId, int shopId)
        {
            Shop shop = shopRepository.Shops
                .Include(s => s.Users)
                .FirstOrDefault(i => i.ShopId == shopId);
            if (shop == null)
            {
                return RedirectToAction(nameof(Shops));
            }
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, shop, "DostępDoSklepów");
            if (authorized.Succeeded)
            {
                ViewBag.ShopId = shopId;
                ViewBag.MenuCategoryId = menuCategoryId;
                return View("EditItem", new Item());
            }
            else
            {
                return new ChallengeResult();
            }
        }

        [Authorize(Roles = "Administratorzy, Kierownicy")]
        public async Task<IActionResult> DeleteItem(int itemId, int shopId)
        {
            Shop shop = shopRepository.Shops
                .Include(s => s.Users)
                .FirstOrDefault(i => i.ShopId == shopId);
            if (shop == null)
            {
                return RedirectToAction(nameof(Shops));
            }
            AuthorizationResult authorized = await authService.AuthorizeAsync(User, shop, "DostępDoSklepów");
            if (authorized.Succeeded)
            {
                Item item = itemRepository.DeleteItem(itemId);
                if (item != null)
                {
                    TempData["message"] = $"Usunięto {item.Name}.";
                }
                return RedirectToAction("Shop", new { shopId = shopId });
            }
            else
            {
                return new ChallengeResult();
            }
        }
    }
}
