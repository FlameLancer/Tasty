using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models.Interfaceses
{
    public interface IOpeningTimeRepository
    {
        IQueryable<OpeningTime> OpeningTimes { get; }
        //void SaveOpeningTime(OpeningTime openingTime);
    }
}
