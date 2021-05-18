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
    public class GetFacultyList
    {
        public record Query() : IRequest<IEnumerable<FacultyPreviewModel>>;

        public class Handler : IRequestHandler<Query, IEnumerable<FacultyPreviewModel>>
        {
            private readonly IMongoCollection<Faculty> _facultyCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _facultyCollection = database.GetCollection<Faculty>("Faculties");
            }

            public async Task<IEnumerable<FacultyPreviewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _facultyCollection.AsQueryable()
                    .Select(i => new FacultyPreviewModel
                    {
                        Id = i.Id,
                        ImageUrl = i.ImageUrl,
                        Name = i.Name,
                    }).ToListAsync();
            }
        }
    }
}
