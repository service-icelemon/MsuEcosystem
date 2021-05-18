using Domain.Entitties.MsuInfo;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Services.InfoService.SubjectFeatures.Queries
{
    public class GetSubjectById
    {
        public record Query(string Id) : IRequest<Subject>;

        public class Handler : IRequestHandler<Query, Subject>
        {
            private readonly IMongoCollection<Subject> _subjectCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _subjectCollection = database.GetCollection<Subject>("Subjects");
            }

            public async Task<Subject> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _subjectCollection.Find(i => i.Id == request.Id).FirstOrDefaultAsync();
            }
        }
    }
}
