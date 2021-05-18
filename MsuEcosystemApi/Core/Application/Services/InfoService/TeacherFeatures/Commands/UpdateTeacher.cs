using Domain.Entitties.MsuInfo;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.TeacherFeatures.Commands
{
    public class UpdateTeacher
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
                var result = await _teacherCollection.
                    ReplaceOneAsync(i => i.Id == request.Teacher.Id, request.Teacher);
                return result.IsAcknowledged ? new Response(true, $"Преподаватель успешно обновлён")
                                            : new Response(false, $"Что-то пошло не так");
            }
        }
    }
}
