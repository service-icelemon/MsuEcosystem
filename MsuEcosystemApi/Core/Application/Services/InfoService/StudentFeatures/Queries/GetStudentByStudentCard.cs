using Domain.Entitties.MsuInfo;
using Domain.Entitties.MsuInfo.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.StudentFeatures.Queries
{
    public class GetStudentByStudentCard
    {
        public record Query(int StudentCardId) : IRequest<StudentViewModel>;

        public class Handler : IRequestHandler<Query, StudentViewModel>
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

            public async Task<StudentViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var student = await _studentCollection.Find(i => i.StudentCardId == request.StudentCardId).FirstOrDefaultAsync();
                return new StudentViewModel
                {
                    Id = student.Id,
                    PhotoUrl = student.PhotoUrl,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    FatherName = student.FatherName,
                    GroupNumber = student.GroupNumber,
                    AdmissionYear = student.AdmissionYear,
                    Faculty = _facultyCollection.Find(f => f.Id == student.FacultyId).FirstOrDefault().Name,
                    EducationForm = _educationFormCollection.Find(e => e.Id == student.EducationFormId).FirstOrDefault(),
                    Specialty = _specialityCollection.Find(s => s.Id == student.SpecialtyId).FirstOrDefault().Name,
                    StudentCardId = student.StudentCardId
                };
            }
        }
    }
}
