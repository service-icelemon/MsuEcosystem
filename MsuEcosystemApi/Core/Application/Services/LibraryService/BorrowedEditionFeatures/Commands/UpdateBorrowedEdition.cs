using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.BorrowedEditionFeatures.Commands
{
    public class UpdateBorrowedEdition
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
                var borrowedEdition = await _borrowedEditionRepository.GetAsync(request.BorrowedEdition.Id);
                if (borrowedEdition == null)
                {
                    return new Response(false, "ошибка");
                }
                borrowedEdition.RequestId = request.BorrowedEdition.RequestId;
                borrowedEdition.BorrowDate = request.BorrowedEdition.BorrowDate;
                borrowedEdition.ReturnDate = request.BorrowedEdition.ReturnDate;
                borrowedEdition.ActualReturnDate = request.BorrowedEdition.ActualReturnDate;
                _borrowedEditionRepository.Update(borrowedEdition);
                return new Response(true, $"Запись о выдаче была удалена");
            }
        }
    }
}
