using Domain.Entitties.MsuInfo;
using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.EducationFormsFeatures.Queries
{
    public class GetEducationFormList
    {
        public record Query() : IRequest<IEnumerable<EducationForm>>;

        public class Handler : IRequestHandler<Query, IEnumerable<EducationForm>>
        {
            private readonly IMongoCollection<EducationForm> _educationFormsCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _educationFormsCollection = database.GetCollection<EducationForm>("EducationForms");
            }

            public async Task<IEnumerable<EducationForm>> Handle(Query request, CancellationToken cancellationToken)
            {
                return _educationFormsCollection.AsQueryable().ToList();
            }
        }
    }
}
