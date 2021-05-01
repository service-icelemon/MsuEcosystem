using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.EditionRequestFeatures.Queries
{
    public class GetEditionRequestList
    {
        public record Query() : IRequest<IEnumerable<EditionRequest>>;

        public class Handler : IRequestHandler<Query, IEnumerable<EditionRequest>>
        {
            private readonly IRepository<EditionRequest> _editionRequestRepository;

            public Handler(IRepository<EditionRequest> editionRequestRepository)
            {
                _editionRequestRepository = editionRequestRepository;
            }

            public async Task<IEnumerable<EditionRequest>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _editionRequestRepository.GetAsync();
            }
        }
    }
}
