using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.GenreService.Commands
{
    public class DeleteGenre
    {
        public record Command(string Id) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<Genre> _genreRepository;

            public Handler(IRepository<Genre> genreRepository)
            {
                _genreRepository = genreRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                _genreRepository.Delete(request.Id);

                return new Response(true, $"Жанр был удалён");
            }
        }
    }
}
