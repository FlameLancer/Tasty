using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models.ViewModels
{
    public class OrdersViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
