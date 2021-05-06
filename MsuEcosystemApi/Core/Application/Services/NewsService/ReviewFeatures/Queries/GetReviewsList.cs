using Domain.Entitties.News;
using Domain.Entitties.News.ViewModels;
using Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.NewsService.ReviewFeatures.Queries
{
    public static class GetReviewsList
    {
        public record Query() : IRequest<IEnumerable<ReviewPreviewModel>>;

        public record Response(bool Succeeded, string Message);
        public class Handler : IRequestHandler<Query, IEnumerable<ReviewPreviewModel>>
        {
            private readonly IRepository<Review> _reviewRepository;

            public Handler(IRepository<Review> reviewRepository)
            {
                _reviewRepository = reviewRepository;
            }

            public async Task<IEnumerable<ReviewPreviewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return (await _reviewRepository.GetAsync(i => i.Draft.IsReviewed))
                    .Select(i => new ReviewPreviewModel
                    {
                        ReviewId = i.ReviewerId,
                        Title = i.EditetTitle,
                        PreviewImageUrl = i.NewPreviewImageUrl
                    });
            }
        }
    }
}
