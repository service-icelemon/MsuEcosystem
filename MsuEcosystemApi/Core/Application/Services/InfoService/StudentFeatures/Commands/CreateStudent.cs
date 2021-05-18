using Domain.Entitties.MsuInfo;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.StudentFeatures.Commands
{
    public class CreateStudent
    {
        public record Command(Student Student) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IMongoCollection<Student> _studentCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _studentCollection = database.GetCollection<Student>("Students");
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                await _studentCollection.InsertOneAsync(request.Student);
                return new Response(true, $"Студент успешно добавлен");
            }
        }
    }
}
