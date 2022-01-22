using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models.Interfaceses
{
    public interface IItemRepository
    {
        IQueryable<Item> Items { get; }
        void SaveItem(Item item);
        Item DeleteItem(int itemId);
    }
}
