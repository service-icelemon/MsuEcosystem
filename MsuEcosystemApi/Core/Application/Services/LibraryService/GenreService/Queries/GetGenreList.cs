using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.GenreService.Queries
{
    public class GetGenreList
    {
        public record Query() : IRequest<IEnumerable<Genre>>;

        public class Handler : IRequestHandler<Query, IEnumerable<Genre>>
        {
            private readonly IRepository<Genre> _genreRepository;

            public Handler(IRepository<Genre> genreRepository)
            {
                _genreRepository = genreRepository;
            }

            public async Task<IEnumerable<Genre>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _genreRepository.GetAsync();
            }
        }
    }
}
