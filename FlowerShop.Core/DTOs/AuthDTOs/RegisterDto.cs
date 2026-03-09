using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.DTOs.AuthDTOs
{
    public class RegisterDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
