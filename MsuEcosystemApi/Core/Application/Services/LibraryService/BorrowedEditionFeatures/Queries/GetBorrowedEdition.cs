using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.BorrowedEditionFeatures.Queries
{
    public class GetBorrowedEdition
    {
        public record Query(string Id) : IRequest<BorrowedEdition>;

        public class Handler : IRequestHandler<Query, BorrowedEdition>
        {
            private readonly IRepository<BorrowedEdition> _borrowedEditionRepository;

            public Handler(IRepository<BorrowedEdition> borrowedEditionRepository)
            {
                _borrowedEditionRepository = borrowedEditionRepository;
            }

            public async Task<BorrowedEdition> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _borrowedEditionRepository.GetAsync(request.Id);
            }
        }
    }
}
