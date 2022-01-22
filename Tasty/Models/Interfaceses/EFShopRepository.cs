using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tasty.Models.Interfaceses
{
    public class EFShopRepository : IShopRepository
    {
        private ApplicationDbContext context;

        public EFShopRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Shop> Shops => context.Shops;

        public void SaveShop(Shop shop)
        {
            if (shop.ShopId == 0)
            {
                context.Shops.Add(shop);
            } 
            else
            {
                Shop dbEntry = context.Shops
                    .Include(s => s.OpeningTimes)
                    .Include(s => s.Categories)
                    .FirstOrDefault(s => s.ShopId == shop.ShopId);
                if (dbEntry != null)
                {
                    dbEntry.Name = shop.Name;
                    dbEntry.MinPrice = shop.MinPrice;
                    dbEntry.TransportPrice = shop.TransportPrice;
                    if(dbEntry.Categories != null && shop.Categories != null)
                        dbEntry.Categories.Clear();
                    dbEntry.Categories = shop.Categories;
                    dbEntry.ShopAddress = shop.ShopAddress;
                    if (dbEntry.OpeningTimes != null && shop.OpeningTimes != null)
                        dbEntry.OpeningTimes.Clear();
                    dbEntry.OpeningTimes = shop.OpeningTimes;
                    dbEntry.PostCodes = shop.PostCodes;
                }
            }
            context.SaveChanges();
        }

        public void ChangeShopStatus(Shop shop)
        {
            context.SaveChanges();
        }

        public Shop DeleteShop(int shopId)
        {
            Shop dbEntry = context.Shops
                .FirstOrDefault(s => s.ShopId == shopId);
            if (dbEntry != null)
            {
                context.Shops.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
