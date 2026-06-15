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
                    CreatedAt TIMESTAMP WITH TIME ZONE NOT NULL,
                    CanCreate BOOLEAN NOT NULL DEFAULT FALSE,
                    CanEdit BOOLEAN NOT NULL DEFAULT FALSE,
                    CanDelete BOOLEAN NOT NULL DEFAULT FALSE,
                    CanAssign BOOLEAN NOT NULL DEFAULT FALSE
                );

                -- Since CREATE TABLE IF NOT EXISTS does not add columns to an already existing table,
                -- we add the missing columns using ALTER TABLE statements if they do not exist.
                ALTER TABLE Users ADD COLUMN IF NOT EXISTS CanCreate BOOLEAN NOT NULL DEFAULT FALSE;
                ALTER TABLE Users ADD COLUMN IF NOT EXISTS CanEdit BOOLEAN NOT NULL DEFAULT FALSE;
                ALTER TABLE Users ADD COLUMN IF NOT EXISTS CanDelete BOOLEAN NOT NULL DEFAULT FALSE;
                ALTER TABLE Users ADD COLUMN IF NOT EXISTS CanAssign BOOLEAN NOT NULL DEFAULT FALSE;

                -- Seed/Update user Tega-123 (and Tega-1234, as seen in database) to make sure they have all authorization permissions.
                UPDATE Users 
                SET CanCreate = TRUE, 
                    CanEdit = TRUE, 
                    CanDelete = TRUE, 
                    CanAssign = TRUE 
                WHERE Username = 'Tega-123' OR Username = 'Tega-1234';
            ");
        }
    }
}
