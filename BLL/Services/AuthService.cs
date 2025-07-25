﻿using AutoMapper;
using BLL.DTO.Requests;
using BLL.DTO.Responces;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<User> userManager, IConfiguration configuration, IMapper mapper, ITokenService tokenService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDTO> AuthAsync(LoginDTO loginDto, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return new AuthResponseDTO
                {
                    IsAuthenticated = false,
                    ErrorMessage = "Пользователь не найден."
                };
            }

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return new AuthResponseDTO
                {
                    IsAuthenticated = false,
                    ErrorMessage = "Неверный пароль."
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.GenerateJwtToken(user, roles);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            var response = _mapper.Map<AuthResponseDTO>(user);
            response.Token = token;
            response.RefreshToken = refreshToken;
            response.IsAuthenticated = true;
            return response;
        }

        public async Task<RegistrationResponseDTO> RegisterAsync(RegisterDTO registerDto, CancellationToken cancellationToken = default)
        {
            if (registerDto == null)
            {
                throw new ValidationException("Необходимо ввести данные пользователя.");
            }

            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);

            if (existingUser != null)
            {
                return new RegistrationResponseDTO
                {
                    Errors = new List<string> { "Пользователь с таким email уже существует." }
                };
            }

            var user = _mapper.Map<User>(registerDto);
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return new RegistrationResponseDTO { Errors = errors };
            }

            await _userManager.AddToRoleAsync(user, "User");
            return new RegistrationResponseDTO { IsRegistered = true };
        }

        public async Task<AuthResponseDTO> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken, cancellationToken);

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new AuthResponseDTO
                {
                    ErrorMessage = "Токен обновления недействителен."
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var newAccessToken = _tokenService.GenerateJwtToken(user, roles);

            return new AuthResponseDTO
            {
                IsAuthenticated = true,
                Token = newAccessToken,
                RefreshToken = request.RefreshToken
            };
        }

        public async Task LogoutAsync(ClaimsPrincipal principal, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.GetUserAsync(principal);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Пользователь не найден.");
            }

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            await _userManager.UpdateAsync(user);
        }
    }
}
