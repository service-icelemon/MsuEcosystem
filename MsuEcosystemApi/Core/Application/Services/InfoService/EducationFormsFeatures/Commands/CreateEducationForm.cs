using Domain.Entitties.MsuInfo;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.EducationFormsFeatures.Commands
{
    public class CreateEducationForm
    {
        public record Command(EducationForm EducationForm) : IRequest<Response>;

        public record Response(bool Successed, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IMongoCollection<EducationForm> _educationFormsCollection;

            public Handler(IMongoClient client)
            {
                var database = client.GetDatabase("MsuInfoDb");
                _educationFormsCollection = database.GetCollection<EducationForm>("EducationForms");
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                await _educationFormsCollection.InsertOneAsync(request.EducationForm);
                return new Response(true, $"Форма обучения успешно добавлена");
            }
        }
    }
}
