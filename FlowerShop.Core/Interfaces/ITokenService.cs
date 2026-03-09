using FlowerShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlowerShop.Core.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(AppUser user);
    }
}
