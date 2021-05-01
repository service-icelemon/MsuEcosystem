using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.PublishingHouseFeatures.Commands
{
    public class CreatePublishingHouse
    {
        public record Command(PublishingHouse PublishingHouse) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<PublishingHouse> _publishingHouseRepository;

            public Handler(IRepository<PublishingHouse> publishingHouseRepository)
            {
                _publishingHouseRepository = publishingHouseRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                string id = Guid.NewGuid().ToString();
                request.PublishingHouse.Id = id;
                await _publishingHouseRepository.CreateAsync(request.PublishingHouse);

                return new Response(true, $"Издательство было добавлено, id - {id}");
            }
        }
    }
}
