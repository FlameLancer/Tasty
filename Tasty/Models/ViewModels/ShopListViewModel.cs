using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Tasty.Models.ViewModels
{
    public class ShopListViewModel
    {
        [Required(ErrorMessage = "Podaj poprawny kod pocztowy")]
        [DataType(DataType.PostalCode)]
        public string Code { get; set; }
        public virtual IEnumerable<Shop> Shops { get; set; }
        public virtual IEnumerable<Shop> ClosedShops { get; set; }
        public virtual IEnumerable<Category> Categories { get; set; }

    }
}
