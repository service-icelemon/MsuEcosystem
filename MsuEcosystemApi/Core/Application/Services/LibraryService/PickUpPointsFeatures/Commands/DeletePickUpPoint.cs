using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.PickUpPointsFeatures.Commands
{
    public class DeletePickUpPoint
    {
        public record Command(string Id) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<PickUpPoint> _pickUpPointRepository;

            public Handler(IRepository<PickUpPoint> pickUpPointRepository)
            {
                _pickUpPointRepository = pickUpPointRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                _pickUpPointRepository.Delete(request.Id);
                return new Response(true, $"Точка выдачи была удалена");
            }
        }
    }
}
