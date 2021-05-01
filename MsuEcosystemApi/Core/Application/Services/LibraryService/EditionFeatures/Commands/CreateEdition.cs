using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.EditionFeatures.Commands
{
    public class CreateEdition
    {
        public record Command(Edition Edition) : IRequest<Response>;
  
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
                string id = Guid.NewGuid().ToString();
                request.Edition.Id = id;
                await _editionRepository.CreateAsync(request.Edition);
                    
                return new Response(true, $"Издание было добавлено, id - {id}");
            }
        }
    }
}
