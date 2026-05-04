using System.Data;

namespace TodoProjectUsingCleanArchitecture.Application.Database.Real
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default);
    }
}
