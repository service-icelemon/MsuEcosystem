using Domain.Entitties.MsuInfo;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.TeacherFeatures.Commands
{
    public class CreateTeacher
    {
        public record Command(Teacher Teacher) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IMongoCollection<Teacher> _teacherCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _teacherCollection = database.GetCollection<Teacher>("Teachers");
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                await _teacherCollection.InsertOneAsync(request.Teacher);
                return new Response(true, $"Преподаватель успешно добавлен");
            }
        }
    }
}
