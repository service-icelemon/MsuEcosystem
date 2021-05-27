using Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserService.Commands
{
    public class Logout
    {
        public record Command(string token) : IRequest<Response>;

        public record Response(bool Succeeded, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IJWTService _jwtService;

            public Handler(IJWTService jwtService)
            {
                _jwtService = jwtService;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = await _jwtService.DeleteRefreshToken(request.token);
                return result ? new Response(true, "выход прошёл успешно")
                              : new Response(false, "что-то пошло не так");
            }
        }
    }
}
