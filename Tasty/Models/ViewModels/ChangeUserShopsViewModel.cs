using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tasty.Models.ViewModels
{
    public class ChangeUserShopsViewModel
    {
        public string Id { get; set; }
        public SelectList Shops { get; set; }
        public IEnumerable<int> SelectedShops { get; set; }
    }
}
