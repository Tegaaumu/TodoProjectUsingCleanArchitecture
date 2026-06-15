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

        public async Task<User?> GetByIdAsync(Guid id)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            return await connection.QueryFirstOrDefaultAsync<User>(
                "SELECT * FROM Users WHERE Id = @Id",
                new { Id = id });
        }

        public async Task<bool> CreateAsync(User user)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var result = await connection.ExecuteAsync(
                @"INSERT INTO Users (Id, Username, Email, PasswordHash, CreatedAt, CanCreate, CanEdit, CanDelete, CanAssign) 
                  VALUES (@Id, @Username, @Email, @PasswordHash, @CreatedAt, @CanCreate, @CanEdit, @CanDelete, @CanAssign)",
                user);
            return result > 0;
        }

        public async Task<bool> UpdatePermissionsAsync(Guid userId, bool canCreate, bool canEdit, bool canDelete, bool canAssign)
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();
            var result = await connection.ExecuteAsync(
                @"UPDATE Users 
                  SET CanCreate = @CanCreate, 
                      CanEdit = @CanEdit, 
                      CanDelete = @CanDelete, 
                      CanAssign = @CanAssign 
                  WHERE Id = @UserId",
                new { UserId = userId, CanCreate = canCreate, CanEdit = canEdit, CanDelete = canDelete, CanAssign = canAssign });
            return result > 0;
        }
    }
}
