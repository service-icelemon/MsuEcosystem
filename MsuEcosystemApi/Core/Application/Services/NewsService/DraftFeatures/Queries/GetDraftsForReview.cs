using Domain.Entitties.News;
using Domain.Entitties.News.ViewModels;
using Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.NewsService.DraftFeatures.Queries
{
    public static class GetDraftsForReview
    {
        public record Query() : IRequest<IEnumerable<DraftPreviewModel>>;

        public class Handler : IRequestHandler<Query, IEnumerable<DraftPreviewModel>>
        {
            private readonly IRepository<Draft> _draftRepository;

            public Handler(IRepository<Draft> draftRepository)
            {
                _draftRepository = draftRepository;
            }

            public async Task<IEnumerable<DraftPreviewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return (await _draftRepository.GetAsync(i => i.IsReadyForReview))
                     .Select(i => new DraftPreviewModel
                     {
                         Id = i.Id,
                         IsReviewed = i.IsReviewed,
                         PreviewImageUrl = i.PreviewImageUrl,
                         Title = i.Title
                     });
            }
        }
    }
}
