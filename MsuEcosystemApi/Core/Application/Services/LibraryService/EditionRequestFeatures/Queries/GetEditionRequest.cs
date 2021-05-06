using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.EditionRequestFeatures.Queries
{
    public class GetEditionRequest
    {
        public record Query(string Id) : IRequest<EditionRequest>;

        public class Handler : IRequestHandler<Query, EditionRequest>
        {
            private readonly IRepository<EditionRequest> _editionRequestRepository;

            public Handler(IRepository<EditionRequest> editionRequestRepository)
            {
                _editionRequestRepository = editionRequestRepository;
            }

            public async Task<EditionRequest> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _editionRequestRepository.GetAsync(request.Id);
            }
        }
    }
}
