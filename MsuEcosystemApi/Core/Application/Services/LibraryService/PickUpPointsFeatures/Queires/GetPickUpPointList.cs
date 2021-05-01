using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.PickUpPointsFeatures.Queires
{
    public class GetPickUpPointList
    {
        public record Query() : IRequest<IEnumerable<PickUpPoint>>;

        public class Handler : IRequestHandler<Query, IEnumerable<PickUpPoint>>
        {
            private readonly IRepository<PickUpPoint> _pickUpPointRepository;

            public Handler(IRepository<PickUpPoint> pickUpPointRepository)
            {
                _pickUpPointRepository = pickUpPointRepository;
            }

            public async Task<IEnumerable<PickUpPoint>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _pickUpPointRepository.GetAsync();
            }
        }
    }
}
