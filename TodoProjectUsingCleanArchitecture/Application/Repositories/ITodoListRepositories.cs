using TodoProjectUsingCleanArchitecture.Application.Models;

namespace TodoProjectUsingCleanArchitecture.Application.Repositories
{
    public interface ITodoListRepositories
    {
        Task<List<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<bool> CreateAync(TaskItem taskItem);
        Task<bool> UpdateAync(TaskItem taskItem);
        Task<bool> DeleteAync(Guid id);
    }
}
