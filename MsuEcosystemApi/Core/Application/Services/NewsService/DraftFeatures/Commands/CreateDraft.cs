using Domain.Entitties.News;
using Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.NewsService.DraftFeatures.Commands
{
    public static class CreateDraft
    {
        public record Command(string Title,
            string Text, string PreviewImageUrl,
            bool IsReadyForReview, string AuthorId) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<Draft> _draftRepository;

            public Handler(IRepository<Draft> draftRepository)
            {
                _draftRepository = draftRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                string id = Guid.NewGuid().ToString();
                await _draftRepository.CreateAsync(
                    new Draft
                    {
                        Id = id,
                        Title = request.Title,
                        Text = request.Text,
                        PreviewImageUrl = request.PreviewImageUrl,
                        AuthorId = request.AuthorId,
                        IsReadyForReview = request.IsReadyForReview,
                        //IsApproved = false,
                        //IsRequiresChanges = false,
                        IsReviewed = false
                    });
                return new Response(true, $"Черновик добавлен, id - {id}");
            }
        }
    }
}
