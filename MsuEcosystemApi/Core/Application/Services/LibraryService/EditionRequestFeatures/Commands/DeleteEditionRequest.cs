using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.EditionRequestFeatures.Commands
{
    public class DeleteEditionRequest
    {
        public record Command(string Id) : IRequest<Response>;

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
                _editionRequestRepository.Delete(request.Id);
                return new Response(true, $"Запрос был удалён");
            }
        }
    }
}
