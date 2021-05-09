using Domain.Entitties.Schedule;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Services.ScheduleService.ClassTypeFeatures.Commands
{
    public class UpdateClassType
    {
        public record Command(ClassType ClassType) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private IMongoCollection<ClassType> _classTypeCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuScheduleDb");
                _classTypeCollection = database.GetCollection<ClassType>("ClassTypes");
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = await _classTypeCollection.
                    ReplaceOneAsync(i => i.Id == request.ClassType.Id, request.ClassType);
                return result.IsAcknowledged ? new Response(true, $"Тип занятия успешно обновлён")
                                            : new Response(false, $"Что-то пошло не так");
            }
        }
    }
}
