using VKTestTask.Domain;
using VKTestTask.Domain.Dto;

namespace VKTestTask.Services.Users;

public interface IUserService
{
    public Task<User> Get(int id);
    public Task<List<User>> GetAll();
    public Task<List<User>> GetFromPage(Page page);
    public Task<User> Create(UserCreationModel user);
    public Task<User> Remove(int id);
}