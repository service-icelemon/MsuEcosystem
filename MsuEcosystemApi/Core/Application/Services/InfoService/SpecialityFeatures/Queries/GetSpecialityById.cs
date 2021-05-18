using Domain.Entitties.MsuInfo;
using Domain.Entitties.MsuInfo.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.SpecialityFeatures.Queries
{
    public class GetSpecialityById
    {
        public record Query(string Id) : IRequest<SpecialityViewModel>;

        public class Handler : IRequestHandler<Query, SpecialityViewModel>
        {
            private readonly IMongoCollection<Speciality> _specialityCollection;
            private readonly IMongoCollection<Subject> _subjectCollection;
            private readonly IMongoCollection<Department> _departmentCollection;
            private readonly IMongoCollection<EducationForm> _educationFormsCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _specialityCollection = database.GetCollection<Speciality>("Specialities");
                _subjectCollection = database.GetCollection<Subject>("Subjects");
                _departmentCollection = database.GetCollection<Department>("Departments");
                _educationFormsCollection = database.GetCollection<EducationForm>("EducationForms");
            }

            public async Task<SpecialityViewModel> Handle(Query request, CancellationToken cancellationToken)
            {
                var speciality = await _specialityCollection.Find(i => i.Id == request.Id).FirstOrDefaultAsync();
                var department = _departmentCollection.Find(i => i.Id == speciality.DepartmentId).FirstOrDefault();
                return new SpecialityViewModel
                {
                    Id = speciality.Id,
                    ImageUrl = speciality.ImageUrl,
                    Description = speciality.Description,
                    Name = speciality.Name,
                    Department = new DepartmentPreviewModel
                    {
                        Id = department.Id,
                        Name = department.Name,
                        ImageUrl = department.ImageUrl
                    },
                    BudgetScores = speciality.BudgetScores,
                    PaidScores = speciality.PaidScores,
                    Subjects = speciality.Subjects
                        .Select(i => new SpecialitySubjectsViewModel
                        {
                            Course = i.Course,
                            Subjects = _subjectCollection
                            .AsQueryable()
                            .Where(s => i.SubjectIds.Contains(s.Id))
                            .ToArray()
                        }).ToArray(),
                    EducationForms = _educationFormsCollection.AsQueryable()
                    .Where(e => speciality.EducationFormIds.Contains(e.Id)).ToArray()
                };
            }
        }
    }
}
