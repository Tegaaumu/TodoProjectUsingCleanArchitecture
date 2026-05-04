using Dapper;
using TodoProjectUsingCleanArchitecture.Application.Database.Real;
using TodoProjectUsingCleanArchitecture.Application.Models;

namespace TodoProjectUsingCleanArchitecture.Application.Repositories
{
    public class DbTodoListRepositories : ITodoListRepositories
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public DbTodoListRepositories(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var items = await connection.QueryAsync<TaskItem>("SELECT * FROM Items");
            return items.ToList();
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            return await connection.QuerySingleOrDefaultAsync<TaskItem>(
                "SELECT * FROM Items WHERE Id = @Id", new { Id = id });
        }

        public async Task<bool> CreateAync(TaskItem taskItem)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var result = await connection.ExecuteAsync(
                @"INSERT INTO Items (Id, Title, IsCompleted, CreatedAt) 
                  VALUES (@Id, @Title, @IsCompleted, @CreatedAt)", 
                taskItem);
            return result > 0;
        }

        public async Task<bool> UpdateAync(TaskItem taskItem)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var result = await connection.ExecuteAsync(
                @"UPDATE Items SET Title = @Title, IsCompleted = @IsCompleted, CreatedAt = @CreatedAt 
                  WHERE Id = @Id", 
                taskItem);
            return result > 0;
        }

        public async Task<bool> DeleteAync(Guid id)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            var result = await connection.ExecuteAsync(
                "DELETE FROM Items WHERE Id = @Id", new { Id = id });
            return result > 0;
        }
    }
}
