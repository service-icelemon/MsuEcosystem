using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.GenreService.Queries
{
    public class GetGenre
    {
        public record Query(string Id) : IRequest<Genre>;

        public class Handler : IRequestHandler<Query, Genre>
        {
            private readonly IRepository<Genre> _genreRepository;

            public Handler(IRepository<Genre> genreRepository)
            {
                _genreRepository = genreRepository;
            }

            public async Task<Genre> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _genreRepository.GetAsync(request.Id);
            }
        }
    }
}
