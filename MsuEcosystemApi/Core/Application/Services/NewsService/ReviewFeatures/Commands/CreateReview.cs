using Domain.Entitties.News;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.NewsService.ReviewFeatures.Commands
{
    public static class CreateReview
    {
        public record Command(string Title, string Text, 
            string PreviewImageUrl, string ReviewText, 
            string DraftId, string ReviewerId) : IRequest<Response>;
        
        public record Response(bool Succeeded, string Message);
        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<Review> _reviewRepository;

            public Handler(IRepository<Review> reviewRepository)
            {
                _reviewRepository = reviewRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                string id = Guid.NewGuid().ToString();
                await _reviewRepository.CreateAsync(
                    new Review
                    {
                        Id = id,
                        EditetTitle = request.Title,
                        EditedText = request.Text,
                        NewPreviewImageUrl = request.PreviewImageUrl,
                        ReviewerId = request.ReviewerId,
                        DraftId = request.DraftId,
                        ReviewText = request.ReviewText
                    });
                return new Response(true, $"Рецензия успешно добавленаб, id - {id}");
            }
        }
    }
}
