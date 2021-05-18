using Domain.Entitties.MsuInfo;
using Domain.Entitties.MsuInfo.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.DepartmentFeatures.Queries
{
    public class GetDepartmentsByFaculty
    {
        public record Query(string FacultyId) : IRequest<IEnumerable<DepartmentPreviewModel>>;

        public class Handler : IRequestHandler<Query, IEnumerable<DepartmentPreviewModel>>
        {
            private readonly IMongoCollection<Department> _departmentCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _departmentCollection = database.GetCollection<Department>("Departments");
            }

            public async Task<IEnumerable<DepartmentPreviewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _departmentCollection.AsQueryable()
                    .Where(i => i.FacultyId == request.FacultyId)
                    .Select(i =>
                    new DepartmentPreviewModel
                    {
                        Id = i.Id,
                        ImageUrl = i.ImageUrl,
                        Name = i.Name,
                    }).ToListAsync();
            }
        }
    }
}
