using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.BorrowedEditionFeatures.Queries
{
    public class GetBorrowedEditionsList
    {
        public record Query() : IRequest<IEnumerable<BorrowedEdition>>;

        public class Handler : IRequestHandler<Query, IEnumerable<BorrowedEdition>>
        {
            private readonly IRepository<BorrowedEdition> _borrowedEditionRepository;

            public Handler(IRepository<BorrowedEdition> borrowedEditionRepository)
            {
                _borrowedEditionRepository = borrowedEditionRepository;
            }

            public async Task<IEnumerable<BorrowedEdition>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _borrowedEditionRepository.GetAsync();
            }
        }
    }
}
