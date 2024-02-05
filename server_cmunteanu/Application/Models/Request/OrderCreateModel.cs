using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Attributes;

namespace Application.Models.Request
{
    public class OrderCreateModel
    {
        [Required]
        [Date(ErrorMessage = "Invalid date!")]
        public DateTime Date { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address!")]
        [StringLength(256)]
        public string PlacedByEmail{ get; set; } = null!;

        [Required]
        [MinLength(1)]
        public List<OrderProductModel> OrderedProducts { get; set; } = null!;
    }
}
