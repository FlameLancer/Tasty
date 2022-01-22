using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models.ViewModels
{
    public class UsersWithPagesViewModel
    {
        public IEnumerable<UsersViewModel> Users { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
