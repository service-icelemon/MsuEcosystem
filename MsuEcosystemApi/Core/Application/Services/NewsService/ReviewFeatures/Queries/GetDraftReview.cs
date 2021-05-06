using Domain.Entitties.News;
using Domain.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.NewsService.ReviewFeatures.Queries
{
    public static class GetDraftReview
    {
        public record Query(string DraftId) : IRequest<Review>;

        public class Handler : IRequestHandler<Query, Review>
        {
            private readonly IRepository<Review> _reviewRepository;

            public Handler(IRepository<Review> reviewRepository)
            {
                _reviewRepository = reviewRepository;
            }

            public async Task<Review> Handle(Query request, CancellationToken cancellationToken)
            {
                return _reviewRepository.Get(i => i.DraftId == request.DraftId).FirstOrDefault();
            }
        }
    }
}
