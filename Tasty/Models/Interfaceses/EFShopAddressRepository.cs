using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models.Interfaceses
{
    public class EFShopAddressRepository : IShopAddressRepository
    {
        private ApplicationDbContext context;

        public EFShopAddressRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<ShopAddress> ShopAddresses => context.ShopAddresses;

        public void SaveShopAddress(ShopAddress shopAddress)
        {
            if (shopAddress.ShopAddressId == 0)
            {
                context.ShopAddresses.Add(shopAddress);
            }
            else
            {
                ShopAddress dbEntry = context.ShopAddresses.FirstOrDefault(sa => sa.ShopAddressId == shopAddress.ShopAddressId);
                if (dbEntry != null)
                {
                    dbEntry.PostCode = shopAddress.PostCode;
                    dbEntry.Street = shopAddress.Street;
                    dbEntry.City = shopAddress.City;
                }
            }
            context.SaveChanges();
        }
    }
}
