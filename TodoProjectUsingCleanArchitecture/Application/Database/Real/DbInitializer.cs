using Dapper;

namespace TodoProjectUsingCleanArchitecture.Application.Database.Real
{
    public class DbInitializer
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public DbInitializer(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task InitializeAsync()
        {
            using var connection = await _dbConnectionFactory.CreateConnectionAsync();

            // Matching TaskItem fields: Id (Guid), Title (string), IsCompleted (bool), CreatedAt (DateTime)
            await connection.ExecuteAsync(@"
                CREATE TABLE IF NOT EXISTS Items (
                    Id UUID PRIMARY KEY,
                    Title TEXT NOT NULL,
                    IsCompleted BOOLEAN NOT NULL DEFAULT FALSE,
                    CreatedAt TIMESTAMP WITH TIME ZONE NOT NULL
                );
                
                CREATE TABLE IF NOT EXISTS Users (
                    Id UUID PRIMARY KEY,
                    Username TEXT NOT NULL,
                    Email TEXT NOT NULL UNIQUE,
                    PasswordHash TEXT NOT NULL,
                    CreatedAt TIMESTAMP WITH TIME ZONE NOT NULL
                );");
        }
    }
}
