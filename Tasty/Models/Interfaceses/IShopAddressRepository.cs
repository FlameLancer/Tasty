using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models.Interfaceses
{
    public interface IShopAddressRepository
    {
        IQueryable<ShopAddress> ShopAddresses { get; }
        void SaveShopAddress(ShopAddress shopAddress);
    }
}
