using Domain.Entitties.Schedule;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.ScheduleService.ClassTimeFeatures.Queries
{
    public class GetClassTimeList
    {
        public record Query() : IRequest<IEnumerable<ClassTime>>;

        public class Handler : IRequestHandler<Query, IEnumerable<ClassTime>>
        {
            private IMongoCollection<ClassTime> _classTimesCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuScheduleDb");
                _classTimesCollection = database.GetCollection<ClassTime>("ClassTimes");
            }

            public async Task<IEnumerable<ClassTime>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _classTimesCollection.Find(new BsonDocument()).ToListAsync();
            }
        }
    }
}
