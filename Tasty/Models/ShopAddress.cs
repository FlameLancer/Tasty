using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models
{
    public class ShopAddress
    {
        public int ShopAddressId { get; set; }

        [Required(ErrorMessage = "Podaj nazwę ulicy.")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Podaj kod pocztowy.")]
        [RegularExpression(@"^(\d{2}-\d{3})$", ErrorMessage = "Podany kod pocztowy jest nieprawidłowy.")]
        public string PostCode { get; set; }
        [Required(ErrorMessage = "Podaj nazwę miasta.")]
        public string City { get; set; }
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }

    }
}
