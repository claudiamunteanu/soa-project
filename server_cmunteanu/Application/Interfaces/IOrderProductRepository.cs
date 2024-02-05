using Domain.Entities;

namespace Application.Repositories.Interfaces
{
    public interface IOrderProductRepository
    {
        Task SaveChanges();
        Task Save(OrderProduct entity);
    }
}
