using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.AuthorFeatures.Commands
{
    public class CreateAuthor
    {
        public record Command(Author Author) : IRequest<Response>;

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
                string id = Guid.NewGuid().ToString();
                request.Author.Id = id;
                await _authorRepository.CreateAsync(request.Author);

                return new Response(true, $"Автор был добавлен, id - {id}");
            }
        }
    }
}
