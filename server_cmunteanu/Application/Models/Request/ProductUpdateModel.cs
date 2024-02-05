using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class ProductUpdateModel
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = null!;

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        [Required]
        [Range(0.1, int.MaxValue)]
        public double Price { get; set; }

        [Required]
        [StringLength(255)]
        public string Photo { get; set; } = null!;
    }
}
