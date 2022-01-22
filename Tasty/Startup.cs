using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasty.Infrastructure;
using Tasty.Models;
using Tasty.Models.Interfaceses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using static Tasty.Infrastructure.ShopAuthorization;

namespace Tasty
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["Data:TastyDb:ConnectionString"]));
            services.AddTransient<IShopRepository, EFShopRepository>();
            services.AddTransient<ICategoryRepository, EFCategoryRepository>();
            services.AddTransient<IItemRepository, EFItemRepository>();
            services.AddTransient<IMenuCategoryRepository, EFMenuCategoryRepository>();
            services.AddTransient<IOpeningTimeRepository, EFOpeningTimeRepository>();
            services.AddTransient<IShopAddressRepository, EFShopAddressRepository>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddTransient<IAuthorizationHandler, ShopAuthorizationHandler>();
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthorization(o =>
            {
                o.AddPolicy("DostêpDoSklepów", policy => {
                    policy.AddRequirements(new ShopAuthorizationRequirement
                    {
                        AllowUsers = true,
                        AllowOthers = true
                    });
                });
            });
            services.AddScoped<SessionCart>(sp => SessionCart.GetSessionCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "shoplist",
                    pattern: "lista-restauracji/{categoryName}",
                    defaults: new { controller = "Home", action = "ShopList" });
                endpoints.MapControllerRoute(
                    name: "shoplist",
                    pattern: "lista-restauracji",
                    defaults: new { controller = "Home", action = "ShopList" });

                endpoints.MapControllerRoute(
                    name: "shop",
                    pattern: "{shopId}/{name}/Menu",
                    defaults: new { controller = "Menu", action = "List" });
                endpoints.MapControllerRoute(
                    name: "shop",
                    pattern: "{shopId}/{name}/Informacje",
                    defaults: new { controller = "Menu", action = "Details" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "",
                    defaults: new { controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(
                    name: "orders",
                    pattern: "Zamowienia/{shopId}/{status}/Strona{page:int}",
                    defaults: new { controller = "Admin", action = "Orders" });
                endpoints.MapControllerRoute(
                    name: "orders",
                    pattern: "Zamowienia/{shopId}/Strona{page:int}",
                    defaults: new { controller = "Admin", action = "Orders", page = 1 });
                endpoints.MapControllerRoute(
                    name: "orders",
                    pattern: "Zamowienia/{shopId}/{status}",
                    defaults: new { controller = "Admin", action = "Orders", page = 1 });
                endpoints.MapControllerRoute(
                    name: "orders",
                    pattern: "Zamowienia/{shopId}",
                    defaults: new { controller = "Admin", action = "Orders", page = 1 });

                endpoints.MapControllerRoute(
                    name: "order",
                    pattern: "Admin/Zamowienie/{orderId}",
                    defaults: new { controller = "Admin", action = "Order"});


                endpoints.MapControllerRoute(
                    name: "order",
                    pattern: "Zamowienie",
                    defaults: new { controller = "Order", action = "Checkout" });
                endpoints.MapControllerRoute(
                    name: "order",
                    pattern: "Zamowienie-zrealizowane",
                    defaults: new { controller = "Order", action = "Completed" });

                endpoints.MapControllerRoute(
                    name: "shops",
                    pattern: "Restauracje/{isActive}/Strona{page:int}",
                    defaults: new { controller = "Admin", action = "Shops" });
                endpoints.MapControllerRoute(
                    name: "shops",
                    pattern: "Restauracje/Strona{page:int}",
                    defaults: new { controller = "Admin", action = "Shops", page = 1 });
                endpoints.MapControllerRoute(
                    name: "shops",
                    pattern: "Restauracje/{isActive}",
                    defaults: new { controller = "Admin", action = "Shops", page = 1 });
                endpoints.MapControllerRoute(
                    name: "shops",
                    pattern: "Restauracje",
                    defaults: new { controller = "Admin", action = "Shops", page = 1 });

                endpoints.MapControllerRoute(
                    name: "shop",
                    pattern: "Restauracja/{shopId}",
                    defaults: new { controller = "Admin", action = "Shop"});
                endpoints.MapControllerRoute(
                    name: "shop",
                    pattern: "Restauracja/Edytuj/{shopId?}",
                    defaults: new { controller = "Admin", action = "EditDetails" });
                endpoints.MapControllerRoute(
                    name: "shop",
                    pattern: "Restauracja/Stworz",
                    defaults: new { controller = "Admin", action = "EditDetails" });
                endpoints.MapControllerRoute(
                    name: "shop",
                    pattern: "Restauracja/Edytuj-kategorie-menu/{menuCategoryId?}",
                    defaults: new { controller = "Admin", action = "EditMenuCategory" });
                endpoints.MapControllerRoute(
                    name: "shop",
                    pattern: "Restauracja/Stworz-kategorie-menu/{shopId?}",
                    defaults: new { controller = "Admin", action = "CreateMenuCategory" });
                endpoints.MapControllerRoute(
                    name: "shop",
                    pattern: "Restauracja/Edytuj-przedmiot/{shopId?}/{itemId?}",
                    defaults: new { controller = "Admin", action = "EditItem" });
                endpoints.MapControllerRoute(
                    name: "shop",
                    pattern: "Restauracja/Stworz-przedmio/{shopId?}/{menuCategoryId?}",
                    defaults: new { controller = "Admin", action = "CreateItem" });

                endpoints.MapControllerRoute(
                    name: "categories",
                    pattern: "Kategorie/Strona{page:int}",
                    defaults: new { controller = "Admin", action = "Categories" });
                endpoints.MapControllerRoute(
                    name: "categories",
                    pattern: "Kategorie",
                    defaults: new { controller = "Admin", action = "Categories", page = 1 });

                endpoints.MapControllerRoute(
                    name: "login",
                    pattern: "Logowanie",
                    defaults: new { controller = "Account", action = "Login" });
                endpoints.MapControllerRoute(
                    name: "login",
                    pattern: "Odmowa-Dostepu",
                    defaults: new { controller = "Account", action = "AccessDenied" });

                endpoints.MapControllerRoute(
                    name: "users",
                    pattern: "Uzytkownicy/Strona{page:int}",
                    defaults: new { controller = "User", action = "Users" });
                endpoints.MapControllerRoute(
                    name: "users",
                    pattern: "Uzytkownicy",
                    defaults: new { controller = "User", action = "Users", page = 1 });
                endpoints.MapControllerRoute(
                    name: "users",
                    pattern: "Uzytkownicy/Role/{id?}",
                    defaults: new { controller = "User", action = "ChangeUserRoles" });
                endpoints.MapControllerRoute(
                    name: "users",
                    pattern: "Uzytkownicy/Restauracje/{id?}",
                    defaults: new { controller = "User", action = "ChangeUserShops" });
                endpoints.MapControllerRoute(
                    name: "users",
                    pattern: "Uzytkownik/{name}",
                    defaults: new { controller = "User", action = "UserDetails" });
                endpoints.MapControllerRoute(
                    name: "users",
                    pattern: "Uzytkownik/Edytuj/{id?}",
                    defaults: new { controller = "User", action = "EditUserDetails" });
                endpoints.MapControllerRoute(
                    name: "users",
                    pattern: "Uzytkownik/Stworz",
                    defaults: new { controller = "User", action = "CreateUser" });

                endpoints.MapControllerRoute(
                    name: "null",
                    pattern: "{controller}/{action}/{id?}");
            });
            SeedData.EnsurePopulated(app);
        }
    }
}
