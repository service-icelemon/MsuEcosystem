using Domain.Entitties.MsuInfo;
using Domain.Entitties.MsuInfo.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Services.InfoService.FacultyFeatures.Queries
{
    public class GetFaculties
    {
        public record Query() : IRequest<IEnumerable<FacultyViewModel>>;

        public class Handler : IRequestHandler<Query, IEnumerable<FacultyViewModel>>
        {
            private readonly IMongoCollection<Teacher> _teachersCollection;
            private readonly IMongoCollection<Faculty> _facultyCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _teachersCollection = database.GetCollection<Teacher>("Teachers");
                _facultyCollection = database.GetCollection<Faculty>("Faculties");
            }

            private TeacherPreviewModel GetTeacherPreiview(string id)
            {
                var teacher = _teachersCollection.Find(t => t.Id == id).FirstOrDefault();
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

            public async Task<IEnumerable<FacultyViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _facultyCollection.AsQueryable()
                    .Select(i => new FacultyViewModel
                    {
                        Id = i.Id,
                        ImageUrl = i.ImageUrl,
                        Description = i.Description,
                        Name = i.Name,
                        Dean = GetTeacherPreiview(i.DeanId)
                    }).ToListAsync();
            }
        }
    }
}
