using AuthService.Application.DTOs.Requests;
using AuthService.Application.DTOs.Responses;

namespace AuthService.Application.Services
{
    public interface IAuthAppService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}