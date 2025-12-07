using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserOrg.Application.Interfaces;
using UserOrg.Domain.Entities;
using UserOrg.Infrastructure.Persistence;

namespace UserOrg.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<User?> GetByIdAsync(int id)
    {
        return _db.Users
            .Include(u => u.Manager)
            .Include(u => u.DirectReports)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<List<User>> GetAllAsync()
    {
        return _db.Users
            .Include(u => u.Manager)
            .ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
    }
}
