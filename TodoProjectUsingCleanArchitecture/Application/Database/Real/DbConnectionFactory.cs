using System.Data;
using Dapper;
using Npgsql;


namespace TodoProjectUsingCleanArchitecture.Application.Database.Real
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("Database:ConnectionString") 
                ?? throw new InvalidOperationException("Connection string 'Database:ConnectionString' not found.");
        }

        public async Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default)
        {
            var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(token);
            return connection;
        }

        /// <summary>
        /// Ensures the database table exists. In a real project, use migrations (FluentMigrator, EF Core Migrations, etc.)
        /// </summary>
        public async Task InitializeAsync()
        {
            using var connection = await CreateConnectionAsync();
            // Create table if it doesn't exist.
            // Matching TaskItem fields: Id (Guid), Title (string), IsCompleted (bool), CreatedAt (DateTime)
            const string sql = @"
                CREATE TABLE IF NOT EXISTS Items (
                    Id UUID PRIMARY KEY,
                    Title TEXT NOT NULL,
                    IsCompleted BOOLEAN NOT NULL DEFAULT FALSE,
                    CreatedAt TIMESTAMP WITH TIME ZONE NOT NULL
                );";
            await connection.ExecuteAsync(sql);
        }
    }
}
