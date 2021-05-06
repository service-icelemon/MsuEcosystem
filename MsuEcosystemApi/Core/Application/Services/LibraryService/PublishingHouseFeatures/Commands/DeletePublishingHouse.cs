using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.PublishingHouseFeatures.Commands
{
    public class DeletePublishingHouse
    {
        public record Command(string Id) : IRequest<Response>;

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
                _publishingHouseRepository.Delete(request.Id);

                return new Response(true, $"Издательство было удалено");
            }
        }
    }
}
