using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models.ViewModels
{
    public class MenuViewModel
    {
        public Shop Shop { get; set; }
        public bool IsOpen { get; set; }
        public string ReturnUrl { get; set; }
    }
}
