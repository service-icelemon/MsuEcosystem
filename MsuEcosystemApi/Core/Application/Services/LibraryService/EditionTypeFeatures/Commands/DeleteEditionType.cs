﻿using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.EditionTypeFeatures.Commands
{
    public class DeleteEditionType
    {
        public record Command(string Id) : IRequest<Response>;

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
                _editionTypeRepository.Delete(request.Id);
                return new Response(true, $"Тип издания был добавлен");
            }
        }
    }
}
