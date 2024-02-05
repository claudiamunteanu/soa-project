using Application.Interfaces;
using Application.Repositories.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Order, int>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Order> GetByIdWithUser(int id)
        {
            var entity = await _dbSet.Include(x => x.PlacedBy)
               .FirstOrDefaultAsync(e => e.Id.Equals(id));
            return entity;
        }
    }
}
