using Domain.Entitties.Identity;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJWTService
    {
        Task<TokenResponse> CreateJwtToken(MsuUser user);
        Task<TokenResponse> RefreshToken(string refreshToken);
        Task<bool> DeleteRefreshToken(string refreshToken);
    }
}
