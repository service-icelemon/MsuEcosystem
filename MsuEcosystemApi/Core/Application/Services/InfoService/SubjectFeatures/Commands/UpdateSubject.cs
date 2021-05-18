using Domain.Entitties.MsuInfo;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Services.InfoService.SubjectFeatures.Commands
{
    public class UpdateSubject
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
                var result = await _subjectCollection.
                    ReplaceOneAsync(i => i.Id == request.Subject.Id, request.Subject);
                return result.IsAcknowledged ? new Response(true, $"Предмет успешно обновлён")
                                            : new Response(false, $"Что-то пошло не так");
            }
        }
    }
}
