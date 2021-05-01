using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.EditionFeatures.Commands
{
    public class UpdateEdition
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
                var edition = await _editionRepository.GetAsync(request.Edition.Id);
                if (edition == null)
                {
                    return new Response(false, "Ошибка");
                }
                edition.AuthorId = request.Edition.AuthorId ?? edition.AuthorId;
                edition.Name = request.Edition.Name ?? edition.Name;
                edition.PublishingHouseId = request.Edition.PublishingHouseId ?? edition.PublishingHouseId;
                edition.TypeId = request.Edition.TypeId ?? edition.TypeId;
                edition.AvaibleAmount = request.Edition.AvaibleAmount;
                edition.GenreId = request.Edition.GenreId ?? edition.GenreId;
                edition.PublishingYear = request.Edition.PublishingYear;
                _editionRepository.Update(edition);
                return new Response(true, $"Издание было обновлено");
            }
        }
    }
}
