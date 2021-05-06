using Domain.Entitties.News;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.NewsService.DraftFeatures.Commands
{
    public static class UpdateDraft
    {
        public record Command(string Title = null,
           string Text = null, string PreviewImageUrl = null,
           bool? IsReadyForReview = null, bool? IsReviewed = null, Draft OldDraft = null) : IRequest<Response>;

        public record Response(bool Succeeded, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<Draft> _draftRepository;

            public Handler(IRepository<Draft> draftRepository)
            {
                _draftRepository = draftRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.OldDraft == null)
                {
                    return new Response(false, "Ошибка");
                }
                request.OldDraft.Text = request.Text ?? request.OldDraft.Text;
                request.OldDraft.Title = request.Title ?? request.OldDraft.Title;
                request.OldDraft.PreviewImageUrl = request.PreviewImageUrl ?? request.OldDraft.PreviewImageUrl;
                request.OldDraft.IsReadyForReview = request.IsReadyForReview ?? request.OldDraft.IsReadyForReview;
                request.OldDraft.IsReviewed = request.IsReviewed ?? request.OldDraft.IsReadyForReview;
                _draftRepository.Update(request.OldDraft);
                return new Response(true, "статья успешно обновлена"); ;
            }

        }
    }
}
