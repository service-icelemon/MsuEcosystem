using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.EditionRequestFeatures.Commands
{
    public class UpdateEditionRequest
    {
        public record Command(EditionRequest EditionRequest) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<EditionRequest> _editionRequestRepository;

            public Handler(IRepository<EditionRequest> editionRequestRepository)
            {
                _editionRequestRepository = editionRequestRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var editionRequest = await _editionRequestRepository.GetAsync(request.EditionRequest.Id);
                if (editionRequest == null)
                { 
                    return new Response(false, $"Ошибка");
                }
                editionRequest.EditionId = request.EditionRequest.EditionId;
                editionRequest.IsPickedUp = request.EditionRequest.IsPickedUp;
                editionRequest.PickUpPointId = request.EditionRequest.PickUpPointId;
                editionRequest.ReaderId = request.EditionRequest.ReaderId;
                editionRequest.Approved = request.EditionRequest.Approved;
                _editionRequestRepository.Update(editionRequest);
                return new Response(true, $"Запрос был обновлён");
            }
        }
    }
}
