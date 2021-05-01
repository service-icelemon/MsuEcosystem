using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.GenreService.Commands
{
    public class UpdateGenre
    {
        public record Command(Genre Genre) : IRequest<Response>;

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
                var genre = await _genreRepository.GetAsync(request.Genre.Id);
                if (genre == null)
                {
                    return new Response(false, "ошибка");
                }
                genre.Name = request.Genre.Name;
                _genreRepository.Update(genre);
                return new Response(true, $"Жанр был обновлён");
            }
        }
    }
}
