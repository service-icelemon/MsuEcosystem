using Domain.Entitties.MsuInfo;
using Domain.Entitties.MsuInfo.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.TeacherFeatures.Queries
{
    public class GetTeacherById
    {
        public record Query(string Id) : IRequest<TeacherViewModel>;

        public class Handler : IRequestHandler<Query, TeacherViewModel>
        {
            private readonly IMongoCollection<Teacher> _teachersCollection;
            private readonly IMongoCollection<Department> _departmentCollection;
            private readonly IMongoCollection<Faculty> _facultyCollection;
            private readonly IMongoCollection<Subject> _subjectCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _teachersCollection = database.GetCollection<Teacher>("Teachers");
                _facultyCollection = database.GetCollection<Faculty>("Faculties");
                _subjectCollection = database.GetCollection<Subject>("Subjects");
                _departmentCollection = database.GetCollection<Department>("Departments");
            }

            public async Task<TeacherViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var teacherData = await _teachersCollection.Find(i => i.Id == request.Id).FirstOrDefaultAsync();
                if (teacherData == null)
                {
                    return null;
                }
                return new TeacherViewModel
                {
                    Id = teacherData.Id,
                    PhotoUrl = teacherData.PhotoUrl,
                    FirstName = teacherData.FirstName,
                    LastName = teacherData.LastName,
                    FatherName = teacherData.FatherName,
                    ScienceDegree = teacherData.ScienceDegree,
                    FacultyName = _facultyCollection.Find(f => f.Id == teacherData.FacultyId).FirstOrDefault().Name,
                    FacultyId = teacherData.FacultyId,
                    DepartmentName = _departmentCollection.Find(d => d.Id == teacherData.DepartmentId).FirstOrDefault().Name,
                    DepartmentId = teacherData.DepartmentId,
                    Subjects = _subjectCollection.AsQueryable().Where(s => teacherData.SubjectIds.Contains(s.Id)).ToArray(),
                    Biography = teacherData.Biography
                };
            }
        }
    }
}
