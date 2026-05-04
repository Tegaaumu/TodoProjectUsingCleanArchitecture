using TodoProjectUsingCleanArchitecture.Application.Models;
using TodoProjectUsingCleanArchitecture.Application.Repositories;
using TodoProjectUsingCleanArchitecture.Application.DTOs;
using Microsoft.AspNetCore.SignalR;
using TodoProjectUsingCleanArchitecture.Presentation.Hubs;

namespace TodoProjectUsingCleanArchitecture.Application.Services
{
    public class TodoListServices : ITodoListServices
    {
        private readonly ITodoListRepositories _todoListRepositories;
        private readonly IHubContext<TodoHub> _hubContext;

        public TodoListServices(ITodoListRepositories todoListRepositories, IHubContext<TodoHub> hubContext)
        {
            _todoListRepositories = todoListRepositories;
            _hubContext = hubContext;
        }
        public async Task<TaskItem> GetByIdAsync(Guid id)
        {
            return await _todoListRepositories.GetByIdAsync(id);
        }
        public async Task<bool> CreateAync(TaskItem taskItem)
        {
            var result = await _todoListRepositories.CreateAync(taskItem);
            if (result)
            {
                var dto = new TaskItemDto { Id = taskItem.Id, Title = taskItem.Title };
                await _hubContext.Clients.All.SendAsync("TodoAdded", dto);
            }
            return result;
        }

        public async Task<bool> DeleteAync(Guid id)
        {
            var result = await _todoListRepositories.DeleteAync(id);
            if (result)
            {
                await _hubContext.Clients.All.SendAsync("TodoDeleted", id);
            }
            return result;
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
            var result = await _todoListRepositories.UpdateAync(taskItem);
            if (result)
            {
                var dto = new TaskItemDto { Id = taskItem.Id, Title = taskItem.Title };
                await _hubContext.Clients.All.SendAsync("TodoUpdated", dto);
            }
            return result;
        }
    }
}
