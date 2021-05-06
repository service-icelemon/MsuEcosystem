using Domain.Entitties.Schedule;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.ScheduleService.ClassTimeFeatures.Commands
{
    public class CreateClassTime
    {
        public record Command(ClassTime ClassTime) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private IMongoCollection<ClassTime> _classTimesCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuScheduleDb");
                _classTimesCollection = database.GetCollection<ClassTime>("ClassTimes");
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                await _classTimesCollection.InsertOneAsync(request.ClassTime);
                return new Response(true, $"Расписание для группы успешно добавлено");
            }
        }
    }
}
