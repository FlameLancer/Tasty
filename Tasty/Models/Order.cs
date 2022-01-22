using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Tasty.Models
{
    public class Order
    {
        public enum Status
        {
            Oczekujace,
            Gotowe,
            Wyslane,
            Dostarczone
        }


        [BindNever]
        public int OrderId { get; set; }
        [BindNever]
        public virtual Shop Shop { get; set; }
        [BindNever]
        public Status DeliveryStatus { get; set; }
        [BindNever]
        public DateTime CreateDate { get; set; }
        [BindNever]
        public virtual ICollection<CartLine> Lines { get; set; }

        [Required(ErrorMessage = "Proszę podąć imię i nazwisko.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Proszę podać adres dostawy.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę miasta.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Proszę podać numer telefonu.")]
        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "Podano zły numer.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Proszę podać kod pocztowy.")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Proszę wybrać czas dostawy.")]
        public TimeSpan DeliveryTime { get; set; }
    }
}
