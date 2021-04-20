using Application.Interfaces;
using Application.Services.JwtService;
using Domain.Entitties.Identity;
using Domain.Entitties.Identity.Settings;
using Domain.Entitties.Identity.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.Contexts;
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
        public record Query(string Email, string Password) : IRequest<Response>;

        public record Response(bool Succeeded, string Message, UserViewModel User, string AccessToken, string RefreshToken);

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly UserManager<MsuUser> _userManager;
            private readonly IJWTService _jwtService;

            public Handler(UserManager<MsuUser> userManager, IJWTService jwtService)
            {
                _userManager = userManager;
                _jwtService = jwtService;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    return new Response(false, "Такого пользователя не существует", null, null, null);
                }
                if (await _userManager.CheckPasswordAsync(user, request.Password))
                {
                    var tokens = await _jwtService.CreateJwtToken(user);
                    var userViewModel = new UserViewModel
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        AvatarImage = user.AvatarImage,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        FatherName = user.FatherName,
                        FacultyId = user.FacultyId,
                        StudentCardId = user.StudentCardId,
                        GroupNumber = user.GroupNumber,
                        Roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false),
                        IsTeacher = user.IsTeacher,
                    };
                    return new Response(true, "успешно", userViewModel, tokens.AccessToken, tokens.RefreshToken);
                }
                return new Response(false, "Неверный пароль", null, null, null);
            }
        }
    }
}
