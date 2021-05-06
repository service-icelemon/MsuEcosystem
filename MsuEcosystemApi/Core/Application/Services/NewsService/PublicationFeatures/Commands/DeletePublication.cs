using Domain.Entitties.News;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.NewsService.PublicationFeatures.Commands
{
    public static class DeletePublication
    {
        public record Command(string Id) : IRequest<Response>;

        public record Response(bool Succeeded, string Message);
        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<Publication> _publicationRepository;

            public Handler(IRepository<Publication> publicationRepository)
            {
                _publicationRepository = publicationRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                _publicationRepository.Delete(request.Id);
                return new Response(true, $"Рецензия успешно удалена");
            }
        }
    }
}
