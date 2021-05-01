using Domain.Entitties.Library;
using Domain.Entitties.Library.ViewModels;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.EditionFeatures.Queries
{
    public class GetEditionList
    {
        public record Query() : IRequest<IEnumerable<EditionViewModel>>;

        public class Handler : IRequestHandler<Query, IEnumerable<EditionViewModel>>
        {
            private readonly IRepository<Edition> _edtionRepository;

            public Handler(IRepository<Edition> edtionRepository)
            {
                _edtionRepository = edtionRepository;
            }

            public async Task<IEnumerable<EditionViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return (await _edtionRepository.GetAsync())
                     .Select(i => new EditionViewModel
                     {
                         Id = i.Id,
                         Name = i.Name,
                         Author = $"{i.Author.FatherName} {i.Author.LastName} {i.Author.FatherName}",
                         Description = i.Description,
                         Genre = i.Genre.Name,
                         PublishingHouse = i.PublishingHouse.Name,
                         AvaibleAmount = i.AvaibleAmount,
                         Type = i.Type.Name,
                         PublishingYear = i.PublishingYear
                     });
            }
        }
    }
}
