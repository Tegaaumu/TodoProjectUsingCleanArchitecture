using TodoProjectUsingCleanArchitecture.Application.Models;

namespace TodoProjectUsingCleanArchitecture.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailOrUsernameAsync(string emailOrUsername);
        Task<bool> CreateAsync(User user);
    }
}
