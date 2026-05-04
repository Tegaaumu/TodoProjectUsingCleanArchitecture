using TodoProjectUsingCleanArchitecture.Application.Models;
using TodoProjectUsingCleanArchitecture.Contract.Request;

namespace TodoProjectUsingCleanArchitecture.Presentation.Mapping
{
    public static class ContractMapping
    {
        public static TaskItem MapToList(this TaskItemRequest request)
        {
            return new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                IsCompleted = request.IsCompleted,
                CreatedAt = DateTime.Now,
            };
        }
        public static TaskItem MapToList(this TaskItemRequest request, Guid id)
        {
            return new TaskItem
            {
                Id = id,
                Title = request.Title,
                IsCompleted = request.IsCompleted,
                CreatedAt = DateTime.Now,
            };
        }
    }
}
