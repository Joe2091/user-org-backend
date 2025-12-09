using System.Collections.Generic;
using System.Threading.Tasks;
using UserOrg.Domain.Entities;

namespace UserOrg.BusinessLogic.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<List<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}
