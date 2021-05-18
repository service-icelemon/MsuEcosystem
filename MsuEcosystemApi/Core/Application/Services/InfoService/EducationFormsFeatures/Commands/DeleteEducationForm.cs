using Domain.Entitties.MsuInfo;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.InfoService.EducationFormsFeatures.Commands
{
    public class DeleteEducationForm
    {
        public record Command(string Id) : IRequest<Response>;

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
                var result = await _educationFormsCollection.DeleteOneAsync(p => p.Id == request.Id);
                return result.IsAcknowledged ? new Response(true, $"Кафедра успешно удалена")
                                             : new Response(false, $"Что-то пошло не так");
            }
        }
    }
}
