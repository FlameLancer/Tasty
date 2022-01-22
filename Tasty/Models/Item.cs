using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Tasty.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        [Required(ErrorMessage = "Podaj nazwe przedmiotu.")]
        public string Name { get; set; }
        public bool isActive { get; set; } = true;

        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(18, 2)")]
        [Required(ErrorMessage = "Podaj cenę przedmiotu.")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public virtual MenuCategory MenuCategory { get; set; }
    }
}
