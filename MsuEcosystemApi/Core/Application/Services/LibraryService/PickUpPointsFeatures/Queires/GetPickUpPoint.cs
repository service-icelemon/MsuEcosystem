using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.PickUpPointsFeatures.Queires
{
    public class GetPickUpPoint
    {
        public record Query(string Id) : IRequest<PickUpPoint>;

        public class Handler : IRequestHandler<Query, PickUpPoint>
        {
            private readonly IRepository<PickUpPoint> _pickUpPointRepository;

            public Handler(IRepository<PickUpPoint> pickUpPointRepository)
            {
                _pickUpPointRepository = pickUpPointRepository;
            }

            public async Task<PickUpPoint> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _pickUpPointRepository.GetAsync(request.Id);
            }
        }
    }
}
