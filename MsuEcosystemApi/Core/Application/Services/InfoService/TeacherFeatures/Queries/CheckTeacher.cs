using Domain.Entitties.MsuInfo;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.TeacherFeatures.Queries
{
    public class CheckTeacher
    {
        public record Query(int TeacherCode) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IMongoCollection<Teacher> _teachersCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _teachersCollection = database.GetCollection<Teacher>("Teachers");
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var teacherData = await _teachersCollection.Find(i => i.Code == request.TeacherCode).FirstOrDefaultAsync();
                return teacherData != null ? new Response(true, "Код действителен")
                                           : new Response(false, "Недействительный код");
            }
        }
    }
}
