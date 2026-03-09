using FlowerShop.Core.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string?> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    }
}
