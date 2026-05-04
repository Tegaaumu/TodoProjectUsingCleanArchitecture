using TodoProjectUsingCleanArchitecture.Application.Models;
using TodoProjectUsingCleanArchitecture.Application.DTOs;

namespace TodoProjectUsingCleanArchitecture.Application.Services
{
    public interface ITodoListServices
    {
        Task<List<TaskItem>> GetAllAsync();
        Task<List<TaskItemDto?>> GetAllByDtoAsync();
        Task<TaskItem> GetByIdAsync(Guid id);
        Task<bool> CreateAync(TaskItem taskItem);
        Task<bool> UpdateAync(TaskItem taskItem);
        Task<bool> DeleteAync(Guid id);
    }
}
