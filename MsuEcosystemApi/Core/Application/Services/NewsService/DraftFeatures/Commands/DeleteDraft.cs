using Domain.Entitties.News;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.NewsService.DraftFeatures.Commands
{
    public static class DeleteDraft
    {
        public record Command(string Id) : IRequest<Response>;

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
                _draftRepository.Delete(request.Id);
                return new Response(true, "статья успешно удалена"); ;
            }

        }
    }
}
