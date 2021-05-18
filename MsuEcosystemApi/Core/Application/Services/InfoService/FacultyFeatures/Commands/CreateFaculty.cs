using Domain.Entitties.MsuInfo;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.FacultyFeatures.Commands
{
    public class CreateFaculty
    {
        public record Command(Faculty Faculty) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IMongoCollection<Faculty> _facultyCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _facultyCollection = database.GetCollection<Faculty>("Faculties");
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                await _facultyCollection.InsertOneAsync(request.Faculty);
                return new Response(true, $"Факультет успешно добавлен");
            }
        }
    }
}
