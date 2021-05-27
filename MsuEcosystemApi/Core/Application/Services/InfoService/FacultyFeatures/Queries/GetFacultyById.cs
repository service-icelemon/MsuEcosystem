using Domain.Entitties.MsuInfo;
using Domain.Entitties.MsuInfo.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.FacultyFeatures.Queries
{
    public class GetFacultyById
    {
        public record Query(string Id) : IRequest<FacultyViewModel>;

        public class Handler : IRequestHandler<Query, FacultyViewModel>
        {
            private readonly IMongoCollection<Teacher> _teachersCollection;
            private readonly IMongoCollection<Faculty> _facultyCollection;
            private readonly IMongoCollection<Department> _departmentCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _teachersCollection = database.GetCollection<Teacher>("Teachers");
                _facultyCollection = database.GetCollection<Faculty>("Faculties");
                _departmentCollection = database.GetCollection<Department>("Departments");
            }

            private TeacherPreviewModel GetTeacherPreiview(string id)
            {
                var teacher = _teachersCollection.Find(t => t.Id == id).FirstOrDefault();
                if (teacher == null)
                {
                    return null;
                }
                return new TeacherPreviewModel
                {
                    Id = teacher.Id,
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    FatherName = teacher.FatherName,
                    PhotoUrl = teacher.PhotoUrl,
                    ScienceDegree = teacher.ScienceDegree
                };
            }

            public async Task<FacultyViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var faculty = await _facultyCollection.Find(i => i.Id == request.Id).FirstOrDefaultAsync();
                return new FacultyViewModel
                {
                    Id = faculty.Id,
                    ImageUrl = faculty.ImageUrl,
                    Description = faculty.Description,
                    Name = faculty.Name,
                    Dean = GetTeacherPreiview(faculty.DeanId),
                    Departments = _departmentCollection.AsQueryable()
                                .Where(d => d.FacultyId == faculty.Id)
                                .Select(d => new DepartmentPreviewModel
                                {
                                    Id = d.Id,
                                    Name = d.Name,
                                    ImageUrl = d.ImageUrl
                                }).ToArray()
                };
            }
        }
    }
}
