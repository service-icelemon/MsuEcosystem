using Domain.Entitties.Identity;
using Domain.Entitties.Identity.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.UserService.Queries
{
    public static class GetUsersList
    {
        public record Query(Expression<Func<MsuUser, bool>> expression) : IRequest<IEnumerable<UserViewModel>>;

        public record Response(bool Succeeded, string Message, IEnumerable<UserViewModel> Users);

        public class Handler : IRequestHandler<Query, IEnumerable<UserViewModel>>
        {
            private readonly UserManager<MsuUser> _userManager;

            public Handler(UserManager<MsuUser> userManager)
            {
                _userManager = userManager;
            }

            public async Task<IEnumerable<UserViewModel>> Handle(Query request, CancellationToken cancellationToken)
            {
                var students = new List<UserViewModel>();
                var result = _userManager.Users.Where(request.expression)
                    .Select(i => new UserViewModel
                    {
                        Id = i.Id,
                        UserName = i.UserName,
                        FatherName = i.FatherName,
                        FirstName = i.FirstName,
                        LastName = i.LastName,
                        StudentCardId = i.StudentCardId,
                        GroupNumber = i.GroupNumber,
                        FacultyId = i.FacultyId,
                        IsTeacher = i.IsTeacher,
                        Email = i.Email,
                        Roles = _userManager.GetRolesAsync(i).Result
                    });
                return result;
            }
        }
    }
}
