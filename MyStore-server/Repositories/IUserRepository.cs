using MyStore.Models;

namespace MyStore.Repositories;

public interface IUserRepository
{
    public Task<User?> GetByLoginAsync(string login);
    public Task<User?> GetByIdAsync(int id);
    public Task<User> AddAsync(User user);
    public Task<User> UpdateAsync(User user);
    public Task<bool> ExistsByEmailAsync(string email);
    public Task<bool> ExistsByUsernameAsync(string username);
}