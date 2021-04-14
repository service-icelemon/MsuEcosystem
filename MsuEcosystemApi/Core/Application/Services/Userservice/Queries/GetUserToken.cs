using Domain.Entitties.Identity;
using Domain.Entitties.Identity.Settings;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserService.Queries
{
    public static class GetUserToken
    {
        public record Query(string Email, string Password) : IRequest<AuthenticationModel>;

        public record Response(bool Succeeded, string Message, MsuUser User);

        public class Handler : IRequestHandler<Query, AuthenticationModel>
        {
            private readonly UserManager<MsuUser> _userManager;
            private readonly JWT _jwt;

            public Handler(UserManager<MsuUser> userManager, IOptions<JWT> jwt)
            {
                _userManager = userManager;
                _jwt = jwt.Value;
            }

            private async Task<JwtSecurityToken> CreateJwtToken(MsuUser user)
            {
                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);
                var roleClaims = new List<Claim>();
                for (int i = 0; i < roles.Count; i++)
                {
                    roleClaims.Add(new Claim("roles", roles[i]));
                }
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
                .Union(userClaims)
                .Union(roleClaims);
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _jwt.Issuer,
                    audience: _jwt.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                    signingCredentials: signingCredentials);
                return jwtSecurityToken;
            }

            public async Task<AuthenticationModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var authenticationModel = new AuthenticationModel();
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    authenticationModel.IsAuthenticated = false;
                    authenticationModel.Message = $"No Accounts Registered with {request.Email}.";
                    return authenticationModel;
                }
                if (await _userManager.CheckPasswordAsync(user, request.Password))
                {
                    authenticationModel.IsAuthenticated = true;
                    JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                    authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                    authenticationModel.Email = user.Email;
                    authenticationModel.IsTeacher = user.IsTeacher;
                    authenticationModel.UserName = user.UserName;
                    authenticationModel.AvatarImage = user.AvatarImage;
                    authenticationModel.FirstName = user.FirstName;
                    authenticationModel.LastName = user.LastName;
                    authenticationModel.FatherName = user.FatherName;
                    authenticationModel.FacultyId = user.FacultyId;
                    authenticationModel.StudentCardId = user.StudentCardId;
                    authenticationModel.GroupNumber = user.GroupNumber;
                    var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                    authenticationModel.Roles = rolesList.ToList();
                    return authenticationModel;
                }
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
                return authenticationModel;
            }
        }
    }
}
