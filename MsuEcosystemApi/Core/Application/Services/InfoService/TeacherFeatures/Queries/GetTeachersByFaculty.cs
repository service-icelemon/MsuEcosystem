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
    public class GetTeachersByFaculty
    {
        public record Query(string FacultyId) : IRequest<IEnumerable<TeacherPreviewModel>>;

        public class Handler : IRequestHandler<Query, IEnumerable<TeacherPreviewModel>>
        {
            private readonly IMongoCollection<Teacher> _teachersCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _teachersCollection = database.GetCollection<Teacher>("Teachers");
            }

            public async Task<IEnumerable<TeacherPreviewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var data = await _teachersCollection.Find(i => i.FacultyId == request.FacultyId).ToListAsync();
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
