namespace Domain.Entities;

public partial class Product : Entity<int>
{
    public string Name { get; set; } = null!;
    public int Stock { get; set; }
    public double Price { get; set; }
    public string Photo { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<OrderProduct> OrdersProducts { get; set; } = new List<OrderProduct>();
}
