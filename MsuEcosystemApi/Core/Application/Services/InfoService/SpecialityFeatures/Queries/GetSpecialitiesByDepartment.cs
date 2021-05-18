using Domain.Entitties.MsuInfo;
using Domain.Entitties.MsuInfo.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Services.InfoService.SpecialityFeatures.Queries
{
    public class GetSpecialitiesByDepartment
    {
        public record Query(string DepartmentId) : IRequest<IEnumerable<SpecialityPreviewModel>>;

        public class Handler : IRequestHandler<Query, IEnumerable<SpecialityPreviewModel>>
        {
            private readonly IMongoCollection<Speciality> _specialityCollection;
            private readonly IMongoCollection<Department> _departmentCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _specialityCollection = database.GetCollection<Speciality>("Specialities");
                _departmentCollection = database.GetCollection<Department>("Departments");
            }

            public async Task<IEnumerable<SpecialityPreviewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var specialities = await _specialityCollection.Find(i => i.DepartmentId == request.DepartmentId).ToListAsync();
                var department = _departmentCollection.Find(i => i.Id == request.DepartmentId).FirstOrDefault();
                return specialities.Select(i => new SpecialityPreviewModel
                {
                    Id = i.Id,
                    ImageUrl = i.ImageUrl,
                    Name = i.Name
                });
            }
        }
    }
}
