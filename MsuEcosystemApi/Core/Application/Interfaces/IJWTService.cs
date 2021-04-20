using Domain.Entitties.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJWTService
    {
        Task<TokenResponse> CreateJwtToken(MsuUser user);
        Task<TokenResponse> RefreshToken(string refreshToken);
    }
}
