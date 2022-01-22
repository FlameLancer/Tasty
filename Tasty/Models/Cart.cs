using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public virtual Shop Shop { get; set; }
        public virtual IEnumerable<CartLine> Lines => lineCollection;

        public virtual void AddItem(Item item, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Item.ItemId == item.ItemId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Item = item,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Item item) =>
            lineCollection.RemoveAll(l => l.Item.ItemId == item.ItemId);

        public virtual void ChangeQuantity(Item item, int difference)
        {
            CartLine line = lineCollection
                .Where(c => c.Item.ItemId == item.ItemId)
                .FirstOrDefault();
            if (difference < 0 && line.Quantity == 1)
                RemoveLine(item);
            else
                line.Quantity += difference;
        }

        public virtual decimal ComputeTotalValue() =>
            lineCollection.Sum(e => e.Item.Price * e.Quantity);
        public virtual decimal CheckWithMinPrice()
        {
            if (Shop.MinPrice > ComputeTotalValue())
                return Shop.MinPrice - ComputeTotalValue();
            return 0;
        }
        public virtual decimal ComputeTotalValueWithTransport() =>
            lineCollection.Sum(e => e.Item.Price * e.Quantity + Shop.TransportPrice);

        public virtual void Clear() => lineCollection.Clear();
    }

    public class CartLine
    {
        public int CartLineID { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
    }
}
