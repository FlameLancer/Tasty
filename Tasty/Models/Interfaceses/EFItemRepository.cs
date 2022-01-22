using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models.Interfaceses
{
    public class EFItemRepository : IItemRepository
    {
        private ApplicationDbContext context;

        public EFItemRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Item> Items => context.Items;

        public void SaveItem(Item item)
        {
            if (item.ItemId == 0)
            {
                context.Items.Add(item);
            }
            else
            {
                Item dbEntry = context.Items.FirstOrDefault(i => i.ItemId == item.ItemId);
                if (dbEntry != null)
                {
                    dbEntry.Name = item.Name;
                    dbEntry.MenuCategory = item.MenuCategory;
                    dbEntry.Price = item.Price;
                    dbEntry.Description = item.Description;
                }
            }
            context.SaveChanges();
        }
        public Item DeleteItem(int itemId)
        {
            Item dbEntry = context.Items
                .FirstOrDefault(i => i.ItemId == itemId);
            if (dbEntry != null)
            {
                context.Items.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
