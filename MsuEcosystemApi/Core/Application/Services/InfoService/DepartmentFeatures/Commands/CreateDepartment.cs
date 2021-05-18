using Domain.Entitties.MsuInfo;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.DepartmentFeatures.Commands
{
    public class CreateDepartment
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
                await _departmentCollection.InsertOneAsync(request.Department);
                return new Response(true, $"Кафедра успешно добавлена");
            }
        }
    }
}
