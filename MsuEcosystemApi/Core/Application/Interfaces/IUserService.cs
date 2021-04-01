using Domain.Entitties.Identity;
using Domain.Entitties.Identity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticationModel> GetTokenAsync(TokenRequest request); 
        Task<UserServiceResponse> RegisterAsync(RegisterModel model);
        Task<UserServiceResponse> VerifyEmailAsync(string code);
        Task<UserServiceResponse> RequestPasswordResetAsync(string email);
        Task<UserServiceResponse> ResetPasswordAsync(string code, string password);
        Task<UserServiceResponse> RequestEmailChangeAsync(string userName, string email, string newEmail);
        Task<UserServiceResponse> ChangeEmailAsync(string code);
        Task<MsuUser> GetUserById(string id);
        Task<IEnumerable<StudentViewModel>> GetStudents();
        Task<IEnumerable<TeacherViewModel>> GetTeachers();
        Task<IEnumerable<string>> GetRoles();
        Task<UserServiceResponse> AddRoleAsync(AddRoleModel model);
    }
}
