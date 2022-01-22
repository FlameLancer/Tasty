using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tasty.Models.Interfaceses
{
    public class EFOpeningTimeRepository : IOpeningTimeRepository
    {
        private ApplicationDbContext context;

        public EFOpeningTimeRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<OpeningTime> OpeningTimes => context.OpeningTimes;

        //public void SaveOpeningTime(OpeningTime openingTime)
        //{
        //    Shop shop = context.Shops
        //        .Include(s => s.OpeningTimes)
        //        .FirstOrDefault(s => s.ShopId == openingTime.ShopId);
        //    if (!shop.OpeningTimes.Any(o => o.DayOfWeek == openingTime.DayOfWeek))
        //    {
        //        context.OpeningTimes.Add(openingTime);
        //    }
        //    else
        //    {
        //        OpeningTime dbEntry = context.OpeningTimes.FirstOrDefault(o => o.ShopId == openingTime.ShopId && o.DayOfWeek == openingTime.DayOfWeek);
        //        if (dbEntry != null)
        //        {
        //            dbEntry.Opening = openingTime.Opening;
        //            dbEntry.Closing = openingTime.Closing;
        //        }
        //    }
        //    context.SaveChanges();
        //}
    }
}
