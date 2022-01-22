using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tasty.Models.Interfaceses
{
    public class EFOrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;

        public EFOrderRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Order> Orders => context.Orders
            .Include(o => o.Shop)
            .Include(o => o.Lines)
                .ThenInclude(o => o.Item);

        public void SaveOrder(Order order)
        {
            order.Shop = context.Shops.Where(s => s.ShopId == order.Shop.ShopId).FirstOrDefault();
            context.AttachRange(order.Lines.Select(l => l.Item));
            if (order.OrderId == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
