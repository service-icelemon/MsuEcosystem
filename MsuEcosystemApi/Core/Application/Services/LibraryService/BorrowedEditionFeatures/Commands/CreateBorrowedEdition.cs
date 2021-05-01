using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.BorrowedEditionFeatures.Commands
{
    public class CreateBorrowedEdition
    {
        public record Command(BorrowedEdition BorrowedEdition) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<BorrowedEdition> _borrowedEditionRepository;

            public Handler(IRepository<BorrowedEdition> borrowedEditionRepository)
            {
                _borrowedEditionRepository = borrowedEditionRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                string id = Guid.NewGuid().ToString();
                request.BorrowedEdition.Id = id;
                await _borrowedEditionRepository.CreateAsync(request.BorrowedEdition);

                return new Response(true, $"Выдача книги была зафиксирована, id - {id}");
            }
        }
    }
}
