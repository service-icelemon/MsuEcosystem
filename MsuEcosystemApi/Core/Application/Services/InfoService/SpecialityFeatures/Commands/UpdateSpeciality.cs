using Domain.Entitties.MsuInfo;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.SpecialityFeatures.Commands
{
    public class UpdateSpeciality
    {
        public record Command(Speciality Speciality) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IMongoCollection<Speciality> _specialityCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _specialityCollection = database.GetCollection<Speciality>("Specialities");
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = await _specialityCollection.
                    ReplaceOneAsync(i => i.Id == request.Speciality.Id, request.Speciality);
                return result.IsAcknowledged ? new Response(true, $"Спецальность успешно обновлёна")
                                            : new Response(false, $"Что-то пошло не так");
            }
        }
    }
}
