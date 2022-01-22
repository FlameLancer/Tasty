using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Podaj nazwe kategorii.")]
        public string Name { get; set; }

        public virtual ICollection<Shop> Shops { get; set; }
    }
}
