using Domain.Entitties.Identity;
using Domain.Entitties.Identity.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserService.Queries
{
    public static class GetUsersList
    {
        public record Query(Expression<Func<MsuUser, bool>> expression) : IRequest<IEnumerable<UserPreviewModel>>;

        public record Response(bool Succeeded, string Message, IEnumerable<UserPreviewModel> Users);

        public class Handler : IRequestHandler<Query, IEnumerable<UserPreviewModel>>
        {
            private readonly UserManager<MsuUser> _userManager;

            public Handler(UserManager<MsuUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<IEnumerable<UserPreviewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var students = new List<UserPreviewModel>();
                var result = _userManager.Users.Where(request.expression)
                    .Select(i => new UserPreviewModel
                    {
                        Id = i.Id,
                        UserName = i.UserName,
                    });
                return result;
            }
        }
    }
}
