using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.EditionTypeFeatures.Commands
{
    public class CreateEditionType
    {
        public record Command(EditionType EditionType) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<EditionType> _editionTypeRepository;

            public Handler(IRepository<EditionType> editionTypeRepository)
            {
                _editionTypeRepository = editionTypeRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                string id = Guid.NewGuid().ToString();
                request.EditionType.Id = id;
                await _editionTypeRepository.CreateAsync(request.EditionType);

                return new Response(true, $"Тип издания был добавлен");
            }
        }
    }
}
