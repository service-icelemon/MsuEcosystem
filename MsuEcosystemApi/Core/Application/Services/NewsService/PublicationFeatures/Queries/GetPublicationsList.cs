using Domain.Entitties.News;
using Domain.Entitties.News.ViewModels;
using Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.NewsService.PublicationFeatures.Queries
{
    public static class GetPublicationsList
    {
        public record Query() : IRequest<IEnumerable<PublicationPreviewModel>>;

        public class Handler : IRequestHandler<Query, IEnumerable<PublicationPreviewModel>>
        {
            private readonly IRepository<Publication> _publicationRepository;

            public Handler(IRepository<Publication> publicationRepository)
            {
                _publicationRepository = publicationRepository;
            }

            public async Task<IEnumerable<PublicationPreviewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var reviews = await _publicationRepository.GetAsync();
                return reviews.Select(i => new PublicationPreviewModel
                {
                    Id = i.Id,
                    Title = i.EditedArticle.EditetTitle,
                    PreviewImageUrl = i.EditedArticle.NewPreviewImageUrl,
                    PublicationDate = i.PublicationDate
                });
            }
        }
    }
}