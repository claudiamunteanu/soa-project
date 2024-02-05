using Application.Repositories.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<OrderProduct> _dbSet;
        public OrderProductRepository(ApplicationDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<OrderProduct>();
        }

        public async Task Save(OrderProduct entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
