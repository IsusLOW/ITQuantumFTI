using AuthService.Application.DTOs.Requests;
using AuthService.Application.DTOs.Responses;
using AuthService.Application.Repositories;
using AuthService.Domain.Entities;
using Shared.Common.Exceptions;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace AuthService.Application.Services
{
    public class AuthAppService(
        IAuthRepository repository,
        IMapper mapper,
        ILogger<AuthAppService> logger,
        ITokenService tokenService) : IAuthAppService
    {
        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await repository.FindByAsync(u => u.Email == request.Email);

            if (existingUser != null)
                throw new ConflictException("User with this email already exists");

            var user = mapper.Map<User>(request);
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, request.Password);
            user.IsActive = true;
            user.CreatedAt = DateTime.UtcNow;
            await repository.CreateAsync(user);

            var token = tokenService.GenerateToken(user);
            var refreshToken = tokenService.GenerateRefreshToken();
            var expiresAt = tokenService.GetTokenExpiration();

            logger.LogInformation("User registered successfully: {Email}", user.Email);

            return new AuthResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                ExpiresAt = expiresAt,
                User = mapper.Map<UserDto>(user)
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await repository.FindByAsync(u => u.Email == request.Email)
                ?? throw new NotFoundException("User", request.Email);

            if (!user.IsActive)
                throw new ForbiddenException("User account is deactivated");

            var token = tokenService.GenerateToken(user);
            var refreshToken = tokenService.GenerateRefreshToken();
            var expiresAt = tokenService.GetTokenExpiration();

            logger.LogInformation("User logged in successfully: {Email}", user.Email);

            return new AuthResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                ExpiresAt = expiresAt,
                User = mapper.Map<UserDto>(user)
            };
        }
    }
}