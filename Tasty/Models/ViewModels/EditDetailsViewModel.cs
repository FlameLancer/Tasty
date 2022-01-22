using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tasty.Models.ViewModels
{
    public class EditDetailsViewModel
    {
        public Shop Shop { get; set; }
        public List<OpeningTime> OpeningTimes { get; set; }
        public IEnumerable<int> SelectedCategories { get; set; }
        public SelectList Categories { get; set; }
    }
}
