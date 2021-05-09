using Domain.Entitties.Schedule;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.ScheduleService.ClassTypeFeatures.Queries
{
    public class GetClassTypeList
    {
        public record Query() : IRequest<IEnumerable<ClassType>>;

        public class Handler : IRequestHandler<Query, IEnumerable<ClassType>>
        {
            private IMongoCollection<ClassType> _classTypesCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuScheduleDb");
                _classTypesCollection = database.GetCollection<ClassType>("ClassTypes");
            }

            public async Task<IEnumerable<ClassType>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _classTypesCollection.Find(new BsonDocument()).ToListAsync();
            }
        }
    }
}
