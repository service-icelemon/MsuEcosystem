using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.PublishingHouseFeatures.Commands
{
    public class UpdatePublishingHouse
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
                var publishingHouse = await _publishingHouseRepository.GetAsync(request.PublishingHouse.Id);
                if (publishingHouse == null)
                {
                    return new Response(false, "ошибка");
                }
                publishingHouse.Name = request.PublishingHouse.Name;
                publishingHouse.Description = request.PublishingHouse.Description;
                _publishingHouseRepository.Update(publishingHouse);
                return new Response(true, $"Издательство было обновлено");
            }
        }
    }
}
