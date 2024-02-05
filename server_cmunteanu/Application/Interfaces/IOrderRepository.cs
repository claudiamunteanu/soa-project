using Application.Repositories.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order, int>
    {
        public Task<Order> GetByIdWithUser(int id);
    }
}
