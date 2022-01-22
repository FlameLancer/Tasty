using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models.Interfaceses
{
    public interface IShopRepository
    {
        IQueryable<Shop> Shops { get; }
        void SaveShop(Shop shop);
        void ChangeShopStatus(Shop shop);
        Shop DeleteShop(int shopId);
    }
}
