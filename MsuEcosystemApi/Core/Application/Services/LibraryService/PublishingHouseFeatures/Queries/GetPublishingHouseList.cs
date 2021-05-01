using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Services.LibraryService.PublishingHouseFeatures.Queries
{
    public class GetPublishingHouseList
    {
        public record Query() : IRequest<IEnumerable<PublishingHouse>>;

        public class Handler : IRequestHandler<Query, IEnumerable<PublishingHouse>>
        {
            private readonly IRepository<PublishingHouse> _publishingHouseRepository;

            public Handler(IRepository<PublishingHouse> publishingHouseRepository)
            {
                _publishingHouseRepository = publishingHouseRepository;
            }

            public async Task<IEnumerable<PublishingHouse>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _publishingHouseRepository.GetAsync();
            }
        }
    }
}
