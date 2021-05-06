using Domain.Entitties.Schedule;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.ScheduleService.ScheduleFeatures.Commands
{
    public class DeleteGroupSchedule
    {
        public record Command(string Id) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private IMongoCollection<GroupShedule> _groupScheduleCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuScheduleDb");
                _groupScheduleCollection = database.GetCollection<GroupShedule>("GroupSchedule");
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                await _groupScheduleCollection.DeleteOneAsync(Builders<GroupShedule>.Filter.Eq("_id", request.Id));
                return new Response(true, $"Расписание удалено");
            }
        }
    }
}
