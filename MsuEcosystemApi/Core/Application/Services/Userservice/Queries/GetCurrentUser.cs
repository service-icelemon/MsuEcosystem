using Domain.Entitties.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserService.Queries
{
    public static class GetCurrentUser
    {
        public record Query(ClaimsPrincipal User) : IRequest<Response>;

        public record Response(bool Succeeded, string Message, MsuUser User);

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly UserManager<MsuUser> _userManager;

            public Handler(UserManager<MsuUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var userEmail = request.User.FindFirst(ClaimTypes.Email).Value;
                var user = await _userManager.FindByEmailAsync(userEmail);
                return user != null ?
                    new Response(true, $"Успшено", user) :
                    new Response(false, "Что-то пошло не так", null);
            }
        }
    }
}
