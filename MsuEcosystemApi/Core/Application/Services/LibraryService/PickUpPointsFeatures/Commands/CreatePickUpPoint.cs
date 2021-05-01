using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.PickUpPointsFeatures.Commands
{
    public class CreatePickUpPoint
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
                string id = Guid.NewGuid().ToString();
                request.PickUpPoint.Id = id;
                await _pickUpPointRepository.CreateAsync(request.PickUpPoint);

                return new Response(true, $"Точка выдачи была добавлена, id - {id}");
            }
        }
    }
}
