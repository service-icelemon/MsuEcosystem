using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.EditionFeatures.Commands
{
    public class DeleteEdition
    {
        public record Command(string Id) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<Edition> _editionRepository;

            public Handler(IRepository<Edition> editionRepository)
            {
                _editionRepository = editionRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                _editionRepository.Delete(request.Id);
                return new Response(true, $"Издание было удалено");
            }
        }
    }
}
