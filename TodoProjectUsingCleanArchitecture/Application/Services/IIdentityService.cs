using TodoProjectUsingCleanArchitecture.Contract.Request;
using TodoProjectUsingCleanArchitecture.Contract.Response;

namespace TodoProjectUsingCleanArchitecture.Application.Services
{
    public interface IIdentityService
    {
        Task<AuthResponse?> RegisterAsync(RegisterRequest request);
        Task<AuthResponse?> LoginAsync(LoginRequest request);
    }
}
