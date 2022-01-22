using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Tasty.Models
{
    public static class SeedData
    {
        private const string password = "zaq1@WSX";
        private const string adminRole = "Administratorzy";
        private const string userRole = "Użytkownicy";
        private const string courierRole = "Kurierzy";
        private const string shopManagerRole = "Kierownicy";
        private const string workerRole = "Pracownicy";

        public static async Task AddRole(RoleManager<IdentityRole> roleManager, string role)
        {
            if (await roleManager.FindByNameAsync(role) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public static async Task AddUser(UserManager<AppUser> userManager, string name, string password, string role, string email)
        {
            AppUser user = await userManager.FindByIdAsync(name);
            if (user == null)
            {
                user = new AppUser { UserName = name, Email = email };
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }

        public static async Task AddUserWithShops(UserManager<AppUser> userManager, List<Shop> shops, string name, string password, string role, string email)
        {
            AppUser user = await userManager.FindByIdAsync(name);
            if (user == null)
            {
                user = new AppUser { UserName = name, Shops = shops, Email = email };
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Shops.Any())
            {
                Shop shop1 = new Shop 
                { 
                    Name = "Pierogarnia Pier",
                    MinPrice = 22.49m,
                    TransportPrice = 2.99m,
                    ShopAddress = new ShopAddress(),
                    Categories = new List<Category>(),
                    MenuCategories = new List<MenuCategory>(),
                    OpeningTimes = new List<OpeningTime>(),
                    PostCodes = "35-304 35-305"
                };
                Shop shop2 = new Shop
                {
                    Name = "Pizzeria Amore",
                    MinPrice = 29.99m,
                    TransportPrice = 0m,
                    ShopAddress = new ShopAddress(),
                    Categories = new List<Category>(),
                    MenuCategories = new List<MenuCategory>(),
                    OpeningTimes = new List<OpeningTime>(),
                    PostCodes = "35-304 35-306"
                };
                Shop shop3 = new Shop
                {
                    Name = "City Burger",
                    MinPrice = 17.99m,
                    TransportPrice = 4.99m,
                    ShopAddress = new ShopAddress(),
                    Categories = new List<Category>(),
                    MenuCategories = new List<MenuCategory>(),
                    OpeningTimes = new List<OpeningTime>(),
                    PostCodes = "35-304 35-305 35-306"
                };
                Shop shop4 = new Shop
                {
                    Name = "24/7/7",
                    MinPrice = 22.49m,
                    TransportPrice = 2.99m,
                    ShopAddress = new ShopAddress(),
                    Categories = new List<Category>(),
                    MenuCategories = new List<MenuCategory>(),
                    OpeningTimes = new List<OpeningTime>(),
                    PostCodes = "35-304 35-305"
                };
                ShopAddress shopAddress1 = new ShopAddress
                {
                    City = "Rzeszów",
                    PostCode = "35-234",
                    Shop = new Shop(),
                    Street = "Krakowska 12"
                };
                ShopAddress shopAddress2 = new ShopAddress
                {
                    City = "Rzeszów",
                    PostCode = "35-299",
                    Shop = new Shop(),
                    Street = "Rajtana 92"
                };
                ShopAddress shopAddress3 = new ShopAddress
                {
                    City = "Rzeszów",
                    PostCode = "35-301",
                    Shop = new Shop(),
                    Street = "Lazurowa 56"
                };
                ShopAddress shopAddress4 = new ShopAddress
                {
                    City = "Rzeszów",
                    PostCode = "35-301",
                    Shop = new Shop(),
                    Street = "Lazurowa 247"
                };
                shop1.ShopAddress = shopAddress1;
                shop2.ShopAddress = shopAddress2;
                shop3.ShopAddress = shopAddress3;
                shop4.ShopAddress = shopAddress4;
                Category category1 = new Category
                {
                    Name = "Pierogi",
                };
                Category category2 = new Category
                {
                    Name = "Polska",
                };
                Category category3 = new Category
                {
                    Name = "Pizza",
                };
                Category category4 = new Category
                {
                    Name = "Włoska",
                };
                Category category5 = new Category
                {
                    Name = "Burger",
                };
                Category category6 = new Category
                {
                    Name = "Amerykańska",
                };
                shop1.Categories.Add(category1, category2);
                shop2.Categories.Add(category3, category4);
                shop3.Categories.Add(category5, category6);
                shop4.Categories.Add(category3, category4, category5, category6);
                MenuCategory menuCategory1 = new MenuCategory
                {
                    Name = "Pierogi",
                    Items = new List<Item>(),
                    Priority = 1
                };
                Item item1 = new Item
                {
                    Name = "Pierogi Ruskie",
                    Description = "z ziemniakami i twarogiem",
                    Price = 25.00m
                };
                Item item2 = new Item
                {
                    Name = "Pierogi z Kapustą",
                    Description = "ze świeżą kapustą i grzybami",
                    Price = 22.00m
                };
                menuCategory1.Items.Add(item1, item2);
                MenuCategory menuCategory2 = new MenuCategory
                {
                    Name = "Napoje",
                    Items = new List<Item>(),
                    Priority = 2
                };
                Item item3 = new Item
                {
                    Name = "Kompot",
                    Description = "malinowy",
                    Price = 3.50m
                };
                Item item4 = new Item
                {
                    Name = "Herbata",
                    Description = "",
                    Price = 3.50m
                };
                menuCategory2.Items.Add(item3, item4);
                shop1.MenuCategories.Add(menuCategory1, menuCategory2);
                MenuCategory menuCategory3 = new MenuCategory
                {
                    Name = "Pizza",
                    Items = new List<Item>(),
                    Priority = 1
                };
                Item item5 = new Item
                {
                    Name = "Pizza Margherita",
                    Description = "z sosom, serem i oregano",
                    Price = 22.00m
                };
                Item item6 = new Item
                {
                    Name = "Pizza Grecka",
                    Description = "z sosem, serem, fetą, oliwkami, czosnkiem i oregano",
                    Price = 27.00m
                };
                menuCategory3.Items.Add(item5, item6);
                MenuCategory menuCategory4 = new MenuCategory
                {
                    Name = "Sosy",
                    Items = new List<Item>(),
                    Priority = 2
                };
                Item item7 = new Item
                {
                    Name = "Sos czosnkowy",
                    Description = "",
                    Price = 3.50m
                };
                Item item8 = new Item
                {
                    Name = "Ketchup",
                    Description = "",
                    Price = 3.50m
                };
                menuCategory4.Items.Add(item7, item8);
                shop2.MenuCategories.Add(menuCategory3, menuCategory4);
                MenuCategory menuCategory5 = new MenuCategory
                {
                    Name = "Burgery",
                    Items = new List<Item>(),
                    Priority = 1
                };
                Item item9 = new Item
                {
                    Name = "Cheese Burger",
                    Description = "z serem",
                    Price = 19.00m
                };
                Item item10 = new Item
                {
                    Name = "Bacon Burger",
                    Description = "z boczkiem",
                    Price = 20.00m
                };
                menuCategory5.Items.Add(item9, item10);
                MenuCategory menuCategory6 = new MenuCategory
                {
                    Name = "Napoje",
                    Items = new List<Item>(),
                    Priority = 2
                };
                Item item11 = new Item
                {
                    Name = "Pepsi",
                    Description = "1l",
                    Price = 3.50m
                };
                Item item12 = new Item
                {
                    Name = "Coca-Cola",
                    Description = "1l",
                    Price = 3.50m
                };
                menuCategory6.Items.Add(item11, item12);
                shop3.MenuCategories.Add(menuCategory5, menuCategory6);
                MenuCategory menuCategory7 = new MenuCategory
                {
                    Name = "Dania",
                    Items = new List<Item>(),
                    Priority = 1
                };
                Item item13 = new Item
                {
                    Name = "Pizza Burger",
                    Description = "Tak",
                    Price = 34.50m
                };
                Item item14 = new Item
                {
                    Name = "Burger Pizza",
                    Description = "Nie",
                    Price = 22.50m
                };
                menuCategory7.Items.Add(item13, item14);
                shop4.MenuCategories.Add(menuCategory7);
                OpeningTime openingTime1 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Monday,
                    Opening = new TimeSpan(20, 0, 0), 
                    Closing = new TimeSpan(1, 6, 0, 0)
                };
                OpeningTime openingTime2 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Tuesday,
                    Opening = new TimeSpan(20, 0, 0),
                    Closing = new TimeSpan(1, 6, 0, 0)
                };
                OpeningTime openingTime3 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Wednesday,
                    Opening = new TimeSpan(20, 0, 0),
                    Closing = new TimeSpan(1, 6, 0, 0)
                };
                OpeningTime openingTime4 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Thursday,
                    Opening = new TimeSpan(20, 0, 0),
                    Closing = new TimeSpan(1, 6, 0, 0)
                };
                OpeningTime openingTime5 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Friday,
                    Opening = new TimeSpan(20, 0, 0),
                    Closing = new TimeSpan(1, 6, 0, 0)
                };
                OpeningTime openingTime6 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Saturday,
                    Opening = new TimeSpan(20, 0, 0),
                    Closing = new TimeSpan(1, 6, 0, 0)
                };
                OpeningTime openingTime7 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Sunday,
                    Opening = new TimeSpan(20, 0, 0),
                    Closing = new TimeSpan(1, 6, 0, 0)
                };
                OpeningTime openingTime11 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Monday,
                    Opening = new TimeSpan(6, 0, 0),
                    Closing = new TimeSpan(20, 0, 0)
                };
                OpeningTime openingTime22 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Tuesday,
                    Opening = new TimeSpan(6, 0, 0),
                    Closing = new TimeSpan(20, 0, 0)
                };
                OpeningTime openingTime33 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Wednesday,
                    Opening = new TimeSpan(6, 0, 0),
                    Closing = new TimeSpan(20, 0, 0)
                };
                OpeningTime openingTime44 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Thursday,
                    Opening = new TimeSpan(6, 0, 0),
                    Closing = new TimeSpan(20, 0, 0)
                };
                OpeningTime openingTime55 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Friday,
                    Opening = new TimeSpan(6, 0, 0),
                    Closing = new TimeSpan(20, 0, 0)
                };
                OpeningTime openingTime66 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Saturday,
                };
                OpeningTime openingTime77 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Sunday,
                };
                OpeningTime openingTime111 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Monday,
                    Opening = new TimeSpan(10, 0, 0),
                    Closing = new TimeSpan(22, 0, 0)
                };
                OpeningTime openingTime222 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Tuesday,
                    Opening = new TimeSpan(10, 0, 0),
                    Closing = new TimeSpan(22, 0, 0)
                };
                OpeningTime openingTime333 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Wednesday,
                    Opening = new TimeSpan(10, 0, 0),
                    Closing = new TimeSpan(22, 0, 0)
                };
                OpeningTime openingTime444 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Thursday,
                };
                OpeningTime openingTime555 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Friday,
                    Opening = new TimeSpan(10, 0, 0),
                    Closing = new TimeSpan(22, 0, 0)
                };
                OpeningTime openingTime666 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Saturday,
                    Opening = new TimeSpan(10, 0, 0),
                    Closing = new TimeSpan(22, 0, 0)
                };
                OpeningTime openingTime777 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Sunday,
                };
                OpeningTime openingTime1111 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Monday,
                    Opening = new TimeSpan(0, 0, 0),
                    Closing = new TimeSpan(1 ,0, 0, 0)
                };
                OpeningTime openingTime2222 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Tuesday,
                    Opening = new TimeSpan(0, 0, 0),
                    Closing = new TimeSpan(1, 0, 0, 0)
                };
                OpeningTime openingTime3333 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Wednesday,
                    Opening = new TimeSpan(0, 0, 0),
                    Closing = new TimeSpan(1, 0, 0, 0)
                };
                OpeningTime openingTime4444 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Thursday,
                    Opening = new TimeSpan(0, 0, 0),
                    Closing = new TimeSpan(1, 0, 0, 0)
                };
                OpeningTime openingTime5555 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Friday,
                    Opening = new TimeSpan(0, 0, 0),
                    Closing = new TimeSpan(1, 0, 0, 0)
                };
                OpeningTime openingTime6666 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Saturday,
                    Opening = new TimeSpan(0, 0, 0),
                    Closing = new TimeSpan(1, 0, 0, 0)
                };
                OpeningTime openingTime7777 = new OpeningTime
                {
                    DayOfWeek = DayOfWeek.Sunday,
                    Opening = new TimeSpan(0, 0, 0),
                    Closing = new TimeSpan(1, 0, 0, 0)
                };
                shop1.OpeningTimes.Add(openingTime1, openingTime2, openingTime3, openingTime4, openingTime5, openingTime6, openingTime7);
                shop2.OpeningTimes.Add(openingTime11, openingTime22, openingTime33, openingTime44, openingTime55, openingTime66, openingTime77);
                shop3.OpeningTimes.Add(openingTime111, openingTime222, openingTime333, openingTime444, openingTime555, openingTime666, openingTime777);
                shop4.OpeningTimes.Add(openingTime1111, openingTime2222, openingTime3333, openingTime4444, openingTime5555, openingTime6666, openingTime7777);
                

                UserManager<AppUser> userManager = app.ApplicationServices
                .GetRequiredService<UserManager<AppUser>>();
                RoleManager<IdentityRole> roleManager = app.ApplicationServices
                    .GetRequiredService<RoleManager<IdentityRole>>();

                await AddRole(roleManager, adminRole);
                await AddRole(roleManager, userRole);
                await AddRole(roleManager, courierRole);
                await AddRole(roleManager, shopManagerRole);
                await AddRole(roleManager, workerRole);

                await AddUser(userManager, "Admin", password, adminRole, "Admin@gmail.com");
                await AddUser(userManager, "User", password, userRole, "User@gmail.com");
                await AddUser(userManager, "Courier", password, courierRole, "Courier@gmail.com");
                await AddUserWithShops(userManager, new List<Shop> { shop1, shop2 }, "ShopManager", password, shopManagerRole, "ShopManager@gmail.com");
                await AddUserWithShops(userManager, new List<Shop> { shop3, shop4 }, "Worker", password, workerRole, "Worker@gmail.com");
            }
            context.SaveChanges();
        }
    }
}
