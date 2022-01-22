using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tasty.Models
{
    public class OpeningTime
    {
        public int ShopId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        [Required(ErrorMessage = "Podaj godzinę otwarcia.")]
        [DataType(DataType.Time, ErrorMessage = "Godzina otwarcia została podana w złym formacie")]
        public TimeSpan Opening { get; set; } = new TimeSpan(0, 0, 0);
        [Required(ErrorMessage = "Podaj godzinę otwarcia.")]
        public TimeSpan Closing { get; set; } = new TimeSpan(0, 0, 0);
        public virtual Shop Shop { get; set; }
    }
}
