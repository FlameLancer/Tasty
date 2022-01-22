using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models.ViewModels
{
    public class ShopsViewModel
    {
        public IEnumerable<Shop> Shops { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
