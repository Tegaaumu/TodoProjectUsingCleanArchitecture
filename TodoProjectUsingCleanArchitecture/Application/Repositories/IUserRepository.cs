using TodoProjectUsingCleanArchitecture.Application.Models;

namespace TodoProjectUsingCleanArchitecture.Application.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailOrUsernameAsync(string emailOrUsername);
        Task<User?> GetByIdAsync(Guid id);
        Task<bool> CreateAsync(User user);
        Task<bool> UpdatePermissionsAsync(Guid userId, bool canCreate, bool canEdit, bool canDelete, bool canAssign);
    }
}
