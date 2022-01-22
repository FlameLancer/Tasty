using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models
{
    public class MenuCategory
    {
        public int MenuCategoryId { get; set; }
        public bool isActive { get; set; } = true;

        [Required(ErrorMessage = "Podaj kolejność kategorii menu.")]
        public int Priority { get; set; }

        [Required(ErrorMessage = "Podaj nazwe kategorii menu.")]
        public string Name { get; set; }
        public virtual Shop Shop { get; set; }
        public virtual ICollection<Item> Items { get; set; }

    }
}
