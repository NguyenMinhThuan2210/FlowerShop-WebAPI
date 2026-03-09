using FlowerShop.Core.DTOs.AuthDTOs;
using FlowerShop.Core.Entities;
using FlowerShop.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        public AuthService(IUserRepository userRepo, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _userRepo.GetByUsernameAsync(dto.UserName);
            if (user == null) return null;
            bool isPasswordValid = _passwordHasher.Verify(dto.Password, user.PasswordHash);
            if (!isPasswordValid) return null;
            string token = _tokenService.GenerateToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Message = "Đăng nhập Thành công!"
            };
        }

        public async Task<string?> RegisterAsync(RegisterDto dto)
        {
            if(await _userRepo.ExistsByUsernameAsync(dto.UserName))
                return "Tên đăng nhập đã tồn tại!";
            string hashedPassword = _passwordHasher.Hash(dto.Password);
            var newUser = new AppUser
            {
                UserName = dto.UserName,
                PasswordHash = hashedPassword,
                FullName = dto.FullName,
                Email = dto.Email,
            };
            _userRepo.Add(newUser);
            var success = await _userRepo.SaveChangesAsync();
            if (!success) return "Lỗi khi lưu vào Database!";

            return null; 
        }
    }
}
