using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.EditionTypeFeatures.Queries
{
    public class GetEditionType
    {
        public record Query(string Id) : IRequest<EditionType>;

        public class Handler : IRequestHandler<Query, EditionType>
        {
            private readonly IRepository<EditionType> _editionTypeRepository;

            public Handler(IRepository<EditionType> editionTypeRepository)
            {
                _editionTypeRepository = editionTypeRepository;
            }

            public async Task<EditionType> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _editionTypeRepository.GetAsync(request.Id);
            }
        }
    }
}
