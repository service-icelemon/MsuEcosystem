using Domain.Entitties.MsuInfo;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Services.InfoService.FacultyFeatures.Commands
{
    public class DeleteFaculty
    {
        public record Command(string Id) : IRequest<Response>;

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
                var result = await _facultyCollection.DeleteOneAsync(p => p.Id == request.Id);
                return result.IsAcknowledged ? new Response(true, $"Факультет успешно удалён")
                                             : new Response(false, $"Что-то пошло не так");
            }
        }
    }
}
