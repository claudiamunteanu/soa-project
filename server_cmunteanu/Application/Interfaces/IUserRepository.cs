using Domain.Entities;
using System.Security.Cryptography;

namespace Application.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User, string>
    {
        Task<User> GetByEmail(String email);
    }
}
