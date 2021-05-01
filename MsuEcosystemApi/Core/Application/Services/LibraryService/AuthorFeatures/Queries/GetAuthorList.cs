using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.AuthorFeatures.Queries
{
    public class GetAuthorList
    {
        public record Query() : IRequest<IEnumerable<Author>>;

        public class Handler : IRequestHandler<Query, IEnumerable<Author>>
        {
            private readonly IRepository<Author> _authorRepository;

            public Handler(IRepository<Author> authorRepository)
            {
                _authorRepository = authorRepository;
            }

            public async Task<IEnumerable<Author>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _authorRepository.GetAsync();
            }
        }
    }
}
