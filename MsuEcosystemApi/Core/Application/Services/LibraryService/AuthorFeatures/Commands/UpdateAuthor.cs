using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.AuthorFeatures.Commands
{
    public class UpdateAuthor
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
                var author = await _authorRepository.GetAsync(request.Author.Id);
                if (author == null)
                {
                    return new Response(false, "ошибка");
                }
                author.FirstName = request.Author.FirstName;
                author.LastName = request.Author.LastName;
                author.FatherName = request.Author.FatherName;
                author.BirthDate = request.Author.BirthDate;
                author.Description = request.Author.Description;
                _authorRepository.Update(author);
                return new Response(true, $"Автор был обновлён");
            }
        }
    }
}