using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.EditionRequestFeatures.Commands
{
    public class CreateEditionRequest
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
                string id = Guid.NewGuid().ToString();
                request.EditionRequest.Id = id;
                await _editionRequestRepository.CreateAsync(request.EditionRequest);

                return new Response(true, $"Запрос был добавлен, id - {id}");
            }
        }
    }
}
