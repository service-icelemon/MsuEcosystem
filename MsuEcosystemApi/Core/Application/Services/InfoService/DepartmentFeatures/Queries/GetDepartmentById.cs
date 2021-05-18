using Application.Services.InfoService.TeacherFeatures.Queries;
using Domain.Entitties.MsuInfo;
using Domain.Entitties.MsuInfo.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.DepartmentFeatures.Queries
{
    public class GetDepartmentById
    {
        public record Query(string Id) : IRequest<DepartmentViewModel>;

        public class Handler : IRequestHandler<Query, DepartmentViewModel>
        {
            private readonly IMongoCollection<Department> _departmentCollection;
            private readonly IMediator _mediator;

            public Handler(IMongoClient client, IMediator mediator)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _departmentCollection = database.GetCollection<Department>("Departments");
                _mediator = mediator;
            }

            public async Task<DepartmentViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var department = await _departmentCollection.Find(i => i.Id == request.Id).FirstOrDefaultAsync();
                var teachers = await _mediator.Send(new GetTeachersByDepartment.Query(department.Id));
                return new DepartmentViewModel
                {
                    Id = department.Id,
                    ImageUrl = department.ImageUrl,
                    Name = department.Name,
                    Description = department.Description,
                    Teachers = teachers.ToArray(),

                };
            }
        }
    }
}
