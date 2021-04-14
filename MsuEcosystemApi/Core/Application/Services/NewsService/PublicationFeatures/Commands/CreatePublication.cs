using Domain.Entitties.News;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.NewsService.PublicationFeatures.Commands
{
    public static class CreatePublication
    {
        public record Command(string ReviewId) : IRequest<Response>;

        public record Response(bool Succeeded, string Message);

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IRepository<Publication> _publicationRepository;

            public Handler(IRepository<Publication> publicationRepository)
            {
                _publicationRepository = publicationRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                string id = Guid.NewGuid().ToString();
                await _publicationRepository.CreateAsync(
                    new Publication
                    {
                        Id = id,
                        ReviewId = request.ReviewId,
                        PublicationDate = DateTime.Now
                    });
                return new Response(true, $"Статья опубликована");
            }
        }
    }
}
