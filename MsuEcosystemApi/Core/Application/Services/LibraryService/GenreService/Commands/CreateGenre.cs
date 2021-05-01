using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.GenreService.Commands
{
    public class CreateGenre
    {
        public record Command(Genre Genre) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<Genre> _genreRepository;

            public Handler(IRepository<Genre> genreRepository)
            {
                _genreRepository = genreRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                string id = Guid.NewGuid().ToString();
                request.Genre.Id = id;
                await _genreRepository.CreateAsync(request.Genre);

                return new Response(true, $"Жанр был добавлен, id - {id}");
            }
        }
    }
}
