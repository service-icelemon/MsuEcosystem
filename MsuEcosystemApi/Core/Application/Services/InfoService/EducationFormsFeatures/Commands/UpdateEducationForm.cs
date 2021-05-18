using Domain.Entitties.MsuInfo;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.EducationFormsFeatures.Commands
{
    public class UpdateEducationForm
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
                var result = await _educationFormsCollection.
                    ReplaceOneAsync(i => i.Id == request.EducationForm.Id, request.EducationForm);
                return result.IsAcknowledged ? new Response(true, $"Форма успешно обновлёна")
                                            : new Response(false, $"Что-то пошло не так");
            }
        }
    }
}
