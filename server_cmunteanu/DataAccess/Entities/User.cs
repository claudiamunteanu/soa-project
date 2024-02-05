namespace Domain.Entities;

public partial class User : Entity<string>
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
