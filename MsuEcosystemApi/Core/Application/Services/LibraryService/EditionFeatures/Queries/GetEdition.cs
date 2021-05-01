using Domain.Entitties.Library;
using Domain.Entitties.Library.ViewModels;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.EditionFeatures.Queries
{
    public class GetEdition
    {
        public record Query(string Id) : IRequest<EditionViewModel>;

        public class Handler : IRequestHandler<Query, EditionViewModel>
        {
            private readonly IRepository<Edition> _editionRepository;

            public Handler(IRepository<Edition> editionRepository)
            {
                _editionRepository = editionRepository;
            }

            public async Task<EditionViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var edition = await _editionRepository.GetAsync(request.Id);
                if (edition == null)
                {
                    return null;
                }
                var editionViewModel = new EditionViewModel
                {
                    Id = edition.Id,
                    Name = edition.Name,
                    Author = $"{edition.Author.FatherName} {edition.Author.LastName} {edition.Author.FatherName}",
                    Description = edition.Description,
                    Genre = edition.Genre.Name,
                    PublishingHouse = edition.PublishingHouse.Name,
                    AvaibleAmount = edition.AvaibleAmount,
                    Type = edition.Type.Name,
                    PublishingYear = edition.PublishingYear
                };
                return editionViewModel;
            }
        }
    }
}
