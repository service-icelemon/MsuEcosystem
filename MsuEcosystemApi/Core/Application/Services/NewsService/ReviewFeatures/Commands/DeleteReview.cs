using Domain.Entitties.News;
using Domain.Interfaces;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.NewsService.ReviewFeatures.Commands
{
    public static class DeleteReview
    {
        public record Command(string Id) : IRequest<Response>;

        public record Response(bool Succeeded, string Message);
        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<Publication> _publicationRepository;
            private readonly IRepository<Review> _reviewRepository;

            public Handler(IRepository<Review> reviewRepository, IRepository<Publication> publicationRepository)
            {
                _publicationRepository = publicationRepository;
                _reviewRepository = reviewRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var publication = (await _publicationRepository.GetAsync(i => i.ReviewId == request.Id)).FirstOrDefault();
                if (publication == null)
                {
                    return new Response(false, $"Рецензия не может быть удалена, так как статья, связанная с ней опубликована");
                }
                _reviewRepository.Delete(request.Id);
                return new Response(true, $"Рецензия успешно удалена");
            }
        }
    }
}
