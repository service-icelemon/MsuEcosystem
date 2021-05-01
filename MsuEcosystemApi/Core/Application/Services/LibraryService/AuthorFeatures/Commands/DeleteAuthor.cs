using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.AuthorFeatures.Commands
{
    public class DeleteAuthor
    {
        public record Command(string Id) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<Author> _authorRepository;

            public Handler(IRepository<Author> authorRepository)
            {
                _authorRepository = authorRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                _authorRepository.Delete(request.Id);
                return new Response(true, $"Автор был удалён");
            }
        }
    }
}
