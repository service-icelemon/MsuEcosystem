using Domain.Entitties.MsuInfo;
using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Services.InfoService.SubjectFeatures.Queries
{
    public class GetSubjectList
    {
        public record Query() : IRequest<IEnumerable<Subject>>;

        public class Handler : IRequestHandler<Query, IEnumerable<Subject>>
        {
            private readonly IMongoCollection<Subject> _subjectCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _subjectCollection = database.GetCollection<Subject>("Subjects");
            }

            public async Task<IEnumerable<Subject>> Handle(Query request, CancellationToken cancellationToken)
            {
                return _subjectCollection.AsQueryable().ToList();
            }
        }
    }
}
