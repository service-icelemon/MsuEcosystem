using Domain.Entitties.Schedule;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.ScheduleService.ScheduleFeatures.Commands
{
    public class UpdateGroupSchedule
    {
        public record Command(GroupShedule GroupShedule) : IRequest<Response>;

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
                var result = await _groupScheduleCollection.
                    ReplaceOneAsync(i => i.Id == request.GroupShedule.Id, request.GroupShedule);
                return result.IsAcknowledged ? new Response(true, $"Расписание успешно обновлено")
                                            : new Response(false, $"Что-то пошло не так");
            }
        }
    }
}
