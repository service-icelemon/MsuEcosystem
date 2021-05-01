using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.PublishingHouseFeatures.Queries
{
    public class GetPublishingHouse
    {
        public record Query(string Id) : IRequest<PublishingHouse>;

        public class Handler : IRequestHandler<Query, PublishingHouse>
        {
            private readonly IRepository<PublishingHouse> _publishingHouseRepository;

            public Handler(IRepository<PublishingHouse> publishingHouseRepository)
            {
                _publishingHouseRepository = publishingHouseRepository;
            }

            public async Task<PublishingHouse> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _publishingHouseRepository.GetAsync(request.Id);
            }
        }
    }
}
