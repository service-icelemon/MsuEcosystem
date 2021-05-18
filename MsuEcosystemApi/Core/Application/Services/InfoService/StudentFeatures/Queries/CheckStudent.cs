using Domain.Entitties.MsuInfo;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.StudentFeatures.Queries
{
    public class CheckStudent
    {
        public record Query(int StudentCardId) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IMongoCollection<Student> _studentCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _studentCollection = database.GetCollection<Student>("Students");
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var student = await _studentCollection.Find(i => i.StudentCardId == request.StudentCardId).FirstOrDefaultAsync();
                return student == null ? new Response(true, "Студенческий билет действителен")
                                       : new Response(false, "Студенческий билет недействителен");

            }
        }
    }
}
