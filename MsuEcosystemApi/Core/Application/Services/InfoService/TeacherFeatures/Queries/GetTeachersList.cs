using Domain.Entitties.MsuInfo;
using Domain.Entitties.MsuInfo.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.TeacherFeatures.Queries
{
    public class GetTeachersList
    {
        public record Query() : IRequest<IEnumerable<TeacherViewModel>>;

        public class Handler : IRequestHandler<Query, IEnumerable<TeacherViewModel>>
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

            public async Task<IEnumerable<TeacherViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var data = await _teachersCollection.Find(new BsonDocument()).ToListAsync();
                return data.Select(i => new TeacherViewModel
                {
                    Id = i.Id,
                    PhotoUrl = i.PhotoUrl,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    FatherName = i.FatherName,
                    ScienceDegree = i.ScienceDegree,
                    FacultyName = _facultyCollection.Find(f => f.Id == i.FacultyId).FirstOrDefault().Name,
                    FacultyId = i.FacultyId,
                    DepartmentName = _departmentCollection.Find(d => d.Id == i.DepartmentId).FirstOrDefault().Name,
                    DepartmentId = i.DepartmentId,
                    Subjects = _subjectCollection.AsQueryable().Where(s => i.SubjectIds.Contains(s.Id)).ToArray(),
                    Biography = i.Biography
                });
            }
        }
    }
}
