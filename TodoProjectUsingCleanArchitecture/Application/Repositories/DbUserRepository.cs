using Dapper;
using TodoProjectUsingCleanArchitecture.Application.Database.Real;
using TodoProjectUsingCleanArchitecture.Application.Models;

namespace TodoProjectUsingCleanArchitecture.Application.Repositories
{
    public class DbUserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public DbUserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<User?> GetByEmailOrUsernameAsync(string emailOrUsername)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            return await connection.QueryFirstOrDefaultAsync<User>(
                "SELECT * FROM Users WHERE Email = @Identifier OR Username = @Identifier",
                new { Identifier = emailOrUsername });
        }

        public async Task<bool> CreateAsync(User user)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var result = await connection.ExecuteAsync(
                @"INSERT INTO Users (Id, Username, Email, PasswordHash, CreatedAt) 
                  VALUES (@Id, @Username, @Email, @PasswordHash, @CreatedAt)",
                user);
            return result > 0;
        }
    }
}
