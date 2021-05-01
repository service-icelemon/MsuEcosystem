using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.PickUpPointsFeatures.Commands
{
    public class UpdatePickUpPoint
    {
        public record Command(PickUpPoint PickUpPoint) : IRequest<Response>;

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
                var pickUpPoint = await _pickUpPointRepository.GetAsync(request.PickUpPoint.Id);
                if (pickUpPoint == null)
                {
                    return new Response(false, "ошибка");
                }
                pickUpPoint.Name = request.PickUpPoint.Name;
                pickUpPoint.Location = request.PickUpPoint.Location;
                _pickUpPointRepository.Update(pickUpPoint);
                return new Response(true, $"Точка выдачи была обновлёна");
            }
        }
    }
}
