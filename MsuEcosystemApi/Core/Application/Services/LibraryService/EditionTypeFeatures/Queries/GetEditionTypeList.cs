using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.EditionTypeFeatures.Queries
{
    public class GetEditionTypeList
    {
        public record Query() : IRequest<IEnumerable<EditionType>>;

        public class Handler : IRequestHandler<Query, IEnumerable<EditionType>>
        {
            private readonly IRepository<EditionType> _editionTypeRepository;

            public Handler(IRepository<EditionType> editionTypeRepository)
            {
                _editionTypeRepository = editionTypeRepository;
            }

            public async Task<IEnumerable<EditionType>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _editionTypeRepository.GetAsync();
            }
        }
    }
}
