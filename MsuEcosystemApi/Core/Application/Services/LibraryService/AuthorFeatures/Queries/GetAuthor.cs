using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.AuthorFeatures.Queries
{
    public class GetAuthor
    {
        public record Query(string Id) : IRequest<Author>;

        public class Handler : IRequestHandler<Query, Author>
        {
            private readonly IRepository<Author> _authorRepository;

            public Handler(IRepository<Author> authorRepository)
            {
                _authorRepository = authorRepository;
            }

            public async Task<Author> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _authorRepository.GetAsync(request.Id);
            }
        }
    }
}
