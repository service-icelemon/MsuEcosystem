using Domain.Entitties.MsuInfo;
using Domain.Entitties.MsuInfo.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.TeacherFeatures.Queries
{
    public class GetTeachersByDepartment
    {
        public record Query(string departmentId) : IRequest<IEnumerable<TeacherPreviewModel>>;

        public class Handler : IRequestHandler<Query, IEnumerable<TeacherPreviewModel>>
        {
            private IMongoCollection<Teacher> _teachersCollection;
            private IMongoCollection<Department> _departmentFormCollection;
            private IMongoCollection<Faculty> _facultyCollection;
            private IMongoCollection<Subject> _subjectCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _teachersCollection = database.GetCollection<Teacher>("Teachers");
                _facultyCollection = database.GetCollection<Faculty>("Faculties");
                _subjectCollection = database.GetCollection<Subject>("Subjects");
            }

            public async Task<IEnumerable<TeacherPreviewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var data = await _teachersCollection.Find(i => i.DepartmentId == request.departmentId).ToListAsync();
                return data.Select(i => new TeacherPreviewModel
                {
                    Id = i.Id,
                    PhotoUrl = i.PhotoUrl,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    FatherName = i.FatherName,
                    ScienceDegree = i.ScienceDegree
                });
            }
        }
    }
}
