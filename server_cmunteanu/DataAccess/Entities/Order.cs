using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order : Entity<int>
    {
        public DateTime Date { get; set; }
        public string PlacedById { get; set; } = null!;

        public virtual User PlacedBy { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<OrderProduct> OrdersProducts { get; set; } = new List<OrderProduct>();
    }
}
