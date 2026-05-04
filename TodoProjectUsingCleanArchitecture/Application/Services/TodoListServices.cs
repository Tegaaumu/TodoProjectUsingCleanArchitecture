using TodoProjectUsingCleanArchitecture.Application.Models;
using TodoProjectUsingCleanArchitecture.Application.Repositories;
using TodoProjectUsingCleanArchitecture.Application.DTOs;

namespace TodoProjectUsingCleanArchitecture.Application.Services
{
    public class TodoListServices : ITodoListServices
    {
        private readonly ITodoListRepositories _todoListRepositories;
        public TodoListServices(ITodoListRepositories todoListRepositories)
        {
            _todoListRepositories = todoListRepositories;
        }
        public async Task<TaskItem> GetByIdAsync(Guid id)
        {
            return await _todoListRepositories.GetByIdAsync(id);
        }
        public async Task<bool> CreateAync(TaskItem taskItem)
        {
            return await _todoListRepositories.CreateAync(taskItem);
        }

        public async Task<bool> DeleteAync(Guid id)
        {
            return await _todoListRepositories.DeleteAync(id);
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _todoListRepositories.GetAllAsync();
        }
        public async Task<List<TaskItemDto>> GetAllByDtoAsync()
        {
            var tasks = await _todoListRepositories.GetAllAsync();

            return tasks.Select(t => new TaskItemDto
            {
                Id = t.Id,
                Title = t.Title
            }).ToList();
        }

        public async Task<bool> UpdateAync(TaskItem taskItem)
        {
            return await _todoListRepositories.UpdateAync(taskItem);
        }
    }
}
