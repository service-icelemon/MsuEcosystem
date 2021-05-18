using Domain.Entitties.MsuInfo;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.EducationFormsFeatures.Queries
{
    public class GetEducationFormById
    {
        public record Query(string Id) : IRequest<EducationForm>;

        public class Handler : IRequestHandler<Query, EducationForm>
        {
            private readonly IMongoCollection<EducationForm> _educationFormsCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _educationFormsCollection = database.GetCollection<EducationForm>("EducationForms");
            }

            public async Task<EducationForm> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _educationFormsCollection.Find(i => i.Id == request.Id).FirstOrDefaultAsync();
            }
        }
    }
}
