using Domain.Entitties.MsuInfo;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.DepartmentFeatures.Commands
{
    public class UpdateDepartment
    {
        public record Command(Department Department) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IMongoCollection<Department> _departmentCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _departmentCollection = database.GetCollection<Department>("Departments");
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var result = await _departmentCollection.
                    ReplaceOneAsync(i => i.Id == request.Department.Id, request.Department);
                return result.IsAcknowledged ? new Response(true, $"Кафедра успешно обновлёна")
                                            : new Response(false, $"Что-то пошло не так");
            }
        }
    }
}
