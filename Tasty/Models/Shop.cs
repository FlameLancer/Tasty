using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models
{
    public class Shop
    {
        public int ShopId { get; set; }
        [Required(ErrorMessage = "Podaj nazwę sklepu.")]
        public string Name { get; set; }
        public virtual ShopAddress ShopAddress { get; set; }
        public bool isActive { get; set; } = true;
        [Required(ErrorMessage = "Podaj wartość minimalnej ceny.")]
        [Range(0.00, double.MaxValue, ErrorMessage = "Podaj dodatnią wartość minimalnej ceny.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MinPrice { get; set; }
        [Required(ErrorMessage = "Podaj wartość ceny transportu.")]
        [Range(0.00, double.MaxValue, ErrorMessage = "Podaj dodatnią wartość ceny transportu.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TransportPrice { get; set; }
        [Required(ErrorMessage = "Podaj obsługiwane regiony.")]
        [MaxLength(5000)]
        [RegularExpression(@"^(\d{2}-\d{3}\s?)*$", ErrorMessage = "Któryś z podany kodów pocztowych jest nieprawidłowy.")]
        public string PostCodes { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<OpeningTime> OpeningTimes { get; set; }
        public virtual ICollection<MenuCategory> MenuCategories { get; set; }
        public virtual ICollection<AppUser> Users { get; set; }

        public virtual bool IdEquals(Shop shop)
        {
            return this.ShopId.Equals(shop.ShopId);
        }
    }
}
