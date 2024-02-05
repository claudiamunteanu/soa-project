using System.ComponentModel.DataAnnotations;

namespace Application.Models.Response
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Stock { get; set; }
        public double Price { get; set; }
        public string Photo { get; set; } = null!;

    }
}
