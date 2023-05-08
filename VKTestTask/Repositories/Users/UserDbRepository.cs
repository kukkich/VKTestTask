using Microsoft.EntityFrameworkCore;
using VKTestTask.Domain;

namespace VKTestTask.Repositories.Users;

public class UserDbRepository : IUserRepository
{
    private IQueryable<User> Users => _context.Users
        .Include(x => x.UserState)
        .Where(x => x.UserState.Code != StateCode.Blocked)
        .Include(x => x.UserGroup);

    private readonly ApplicationContext _context;

    public UserDbRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<User> GetById(int id)
    {
        return await Users.FirstAsync(x => x.Id == id);
    }

    public async Task<User?> TryGetById(int id)
    {
        return await Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<User>> GetAll()
    {
        return await Users.ToListAsync();
    }

    public async Task<List<User>> Take(int offset, int count)
    {
        return await Users
            .OrderByDescending(x => x.Id)
            .Skip(offset)
            .Take(count)
            .ToListAsync();
    }

    public async Task<User> Delete(User user)
    {
        var removedEntry = _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return removedEntry.Entity;
    }

    public async Task<User> Update(User user)
    {
        var updatedEntry = _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return updatedEntry.Entity;
    }

    public async Task<bool> IsAdminExist()
    {
        return await Users.AnyAsync(x => x.UserGroup.Code == GroupCode.Admin);
    }

    public async Task<bool> IsLoginExist(string login)
    {
        return await Users.AnyAsync(x => x.Login == login);
    }

    public async Task<User> Add(User user)
    {
        var addedEntry = _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return addedEntry.Entity;
    }
}