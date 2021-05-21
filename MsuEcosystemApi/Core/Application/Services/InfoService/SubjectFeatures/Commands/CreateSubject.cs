using Domain.Entitties.MsuInfo;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.SubjectFeatures.Commands
{
    public class CreateSubject
    {
        public record Command(Subject Subject) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IMongoCollection<Subject> _subjectCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _subjectCollection = database.GetCollection<Subject>("Subjects");
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                await _subjectCollection.InsertOneAsync(request.Subject);
                return new Response(true, $"Предмет успешно добавлен");
            }
        }
    }
}
