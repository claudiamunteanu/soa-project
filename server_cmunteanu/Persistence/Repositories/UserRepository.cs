using Application.Repositories.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class UserRepository : GenericRepository<User, string>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _dbSet
                .FirstOrDefaultAsync(e => e.Email.Equals(email));
            return user;
        }
    }
}
