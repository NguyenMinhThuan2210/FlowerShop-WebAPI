using BCrypt.Net;
using FlowerShop.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Infrastructure.Services
{
    public class BcryptPasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
