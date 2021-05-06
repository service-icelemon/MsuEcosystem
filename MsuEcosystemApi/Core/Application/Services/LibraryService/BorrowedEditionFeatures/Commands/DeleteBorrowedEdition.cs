using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
namespace Application.Services.LibraryService.BorrowedEditionFeatures.Commands
{
    public class DeleteBorrowedEdition
    {
        public record Command(string Id) : IRequest<Response>;

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
                _borrowedEditionRepository.Delete(request.Id);
                return new Response(true, $"Запись о выдаче удалена");
            }
        }
    }
}
