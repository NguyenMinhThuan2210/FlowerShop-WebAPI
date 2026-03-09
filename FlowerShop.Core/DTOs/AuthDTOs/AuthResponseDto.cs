using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.DTOs.AuthDTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
