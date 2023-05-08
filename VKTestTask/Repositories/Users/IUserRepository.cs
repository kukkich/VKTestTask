using System.Linq.Expressions;
using VKTestTask.Domain;

namespace VKTestTask.Repositories.Users;

public interface IUserRepository
{
    public Task<User> GetById(int id);
    public Task<User?> TryGetById(int id);
    public Task<List<User>> GetAll();
    public Task<List<User>> Take(int offset, int count);
    public Task<User> Delete(User user);
    public Task<User> Update(User user);
    public Task<bool> IsAdminExist();
    public Task<bool> IsLoginExist(string predicate);
    public Task<User> Add(User user);
}