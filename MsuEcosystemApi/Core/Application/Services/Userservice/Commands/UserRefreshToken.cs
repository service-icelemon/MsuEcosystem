using Application.Interfaces;
using Application.Services.JwtService;
using Domain.Entitties.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserService.Commands
{
    public class UserRefreshToken
    {
        public record Command(string RefreshToken) : IRequest<Response>;

        public record Response(TokenResponse Tokens, bool Succeeded, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IJWTService _jwtService;

            public Handler(IJWTService jwtService)
            {
                _jwtService = jwtService;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var tokens = await _jwtService.RefreshToken(request.RefreshToken);
                if (tokens != null)
                {
                    return new Response(tokens, true, "токен успешно обновлён");
                }
                return new Response(null, false, "произошла ошибка");
            }
        }
    }
}
