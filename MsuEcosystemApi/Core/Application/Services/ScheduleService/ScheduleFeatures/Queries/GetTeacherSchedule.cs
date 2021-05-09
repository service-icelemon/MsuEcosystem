using Domain.Entitties.Schedule;
using Domain.Entitties.Schedule.ViewModels.TeacherScheduleViewModel;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.ScheduleService.ScheduleFeatures.Queries
{
    class GetTeacherSchedule
    {
        public record Query(string TeacherId) : IRequest<TeacherSchedule>;

        public class Handler : IRequestHandler<Query, TeacherSchedule>
        {
            private IMongoCollection<GroupShedule> _groupScheduleCollection;
            private IMongoCollection<ClassType> _classTypesCollection;
            private IMongoCollection<ClassTime> _classTimesCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuScheduleDb");
                _groupScheduleCollection = database.GetCollection<GroupShedule>("GroupSchedule");
                _classTypesCollection = database.GetCollection<ClassType>("ClassTypes");
                _classTimesCollection = database.GetCollection<ClassTime>("ClassTimes");
            }

            public async Task<TeacherSchedule> Handle(Query request, CancellationToken cancellationToken)
            {
                return new TeacherSchedule();
            }
        }
    }
}
