using Domain.Entitties.MsuInfo;
using Domain.Entitties.MsuInfo.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.StudentFeatures.Queries
{
    public class GetStudentsByFaculty
    {
        public record Query(string FacultyId) : IRequest<IEnumerable<StudentPreviewModel>>;

        public class Handler : IRequestHandler<Query, IEnumerable<StudentPreviewModel>>
        {
            private readonly IMongoCollection<Student> _studentCollection;
            private readonly IMongoCollection<EducationForm> _educationFormCollection;
            private readonly IMongoCollection<Faculty> _facultyCollection;
            private readonly IMongoCollection<Speciality> _specialityCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _studentCollection = database.GetCollection<Student>("Students");
                _educationFormCollection = database.GetCollection<EducationForm>("EducationForms");
                _facultyCollection = database.GetCollection<Faculty>("Faculties");
                _specialityCollection = database.GetCollection<Speciality>("Specialities");
            }

            public async Task<IEnumerable<StudentPreviewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var data = await _studentCollection.Find(i => i.FacultyId == request.FacultyId).ToListAsync();
                return data.Select(i => new StudentPreviewModel
                {
                    Id = i.Id,
                    PhotoUrl = i.PhotoUrl,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    FatherName = i.FatherName,
                    GroupNumber = i.GroupNumber,
                    AdmissionYear = i.AdmissionYear,
                    Faculty = _facultyCollection.Find(f => f.Id == i.FacultyId).FirstOrDefault().Name,
                    EducationForm = _educationFormCollection.Find(e => e.Id == i.EducationFormId).FirstOrDefault(),
                    Specialty = _specialityCollection.Find(s => s.Id == i.SpecialtyId).FirstOrDefault().Name,
                    StudentCardId = i.StudentCardId
                });
            }
        }
    }
}
