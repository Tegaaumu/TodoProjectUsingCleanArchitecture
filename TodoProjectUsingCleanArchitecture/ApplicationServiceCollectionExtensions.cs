using TodoProjectUsingCleanArchitecture.Application.Database.Real;
using TodoProjectUsingCleanArchitecture.Application.Repositories;
using TodoProjectUsingCleanArchitecture.Application.Services;

namespace TodoProjectUsingCleanArchitecture
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) 
        {
            services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            services.AddSingleton<DbInitializer>();
            services.AddSingleton<ITodoListRepositories, DbTodoListRepositories>();
            services.AddSingleton<ITodoListServices, TodoListServices>();
            
            services.AddSingleton<IUserRepository, DbUserRepository>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IIdentityService, IdentityService>();
            return services;

        }
    }
}
