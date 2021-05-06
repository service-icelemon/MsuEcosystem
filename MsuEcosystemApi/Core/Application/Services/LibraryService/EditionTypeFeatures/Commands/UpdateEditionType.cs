using Domain.Entitties.Library;
using Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.LibraryService.EditionTypeFeatures.Commands
{
    public class UpdateEditionType
    {
        public record Command(EditionType EditionType) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<EditionType> _editionTypeRepository;

            public Handler(IRepository<EditionType> editionTypeRepository)
            {
                _editionTypeRepository = editionTypeRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var editionType = await _editionTypeRepository.GetAsync(request.EditionType.Id);
                if (editionType == null)
                {
                    return new Response(false, "ошибка");
                }
                editionType.Name = request.EditionType.Name;
                _editionTypeRepository.Update(editionType);
                return new Response(true, $"Тип издания был обновлён");
            }
        }
    }
}
