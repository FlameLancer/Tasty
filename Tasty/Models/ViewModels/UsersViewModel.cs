using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models.ViewModels
{
    public class UsersViewModel
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public IEnumerable<Shop> Shops { get; set; }

    }
}
