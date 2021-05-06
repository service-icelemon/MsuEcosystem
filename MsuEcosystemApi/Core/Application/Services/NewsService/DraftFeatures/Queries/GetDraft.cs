using Domain.Entitties.News;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.NewsService.DraftFeatures.Queries
{
    public static class GetDraft
    {
        public record Query(string Id) : IRequest<Draft>;

        public class Handler : IRequestHandler<Query, Draft>
        {
            private readonly IRepository<Draft> _draftRepository;

            public Handler(IRepository<Draft> draftRepository)
            {
                _draftRepository = draftRepository;
            }

            public async Task<Draft> Handle(Query request, CancellationToken cancellationToken)
            {
                var draft = await _draftRepository.GetAsync(request.Id);
                if (draft == null)
                {
                    return null;
                }
                return draft;
            }
        }
    }
}
