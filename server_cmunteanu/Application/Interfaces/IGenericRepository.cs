using Domain.Entities;

namespace Application.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity, TId> where TEntity : Entity<TId>
    {
        Task Save(TEntity entity);
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(TId id);
        Task Update(TEntity entity);
        Task Delete(TId id);
        Task SaveChanges();
    }
}
