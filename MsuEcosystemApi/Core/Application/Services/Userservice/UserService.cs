using Application.Interfaces;
using Domain.Entitties.Identity;
using Domain.Entitties.Identity.Settings;
using Domain.Entitties.Identity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.Constants;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Userservice
{
    public class UserService : IUserService
    {
        private readonly UserManager<MsuUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;

        public UserService(UserManager<MsuUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }

        public async Task<MsuUser> GetCurrentUser(ClaimsPrincipal user)
        {
            var userEmail = user.FindFirst(ClaimTypes.Email).Value;
            return await _userManager.FindByEmailAsync(userEmail);
        }

        public async Task<UserViewModel> GetUserById(string id)
        {
            var result = await _userManager.FindByIdAsync(id);
            if (result != null)
            {
                return new UserViewModel
                {
                    Id = result.Id,
                    UserName = result.UserName,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    FatherName = result.FatherName,
                    Email = result.Email,
                    StudentCardId = result.StudentCardId,
                    FacultyId = result.FacultyId,
                    GroupNumber = result.GroupNumber
                };
            }
            return null;
        }


        public async Task<UserServiceResponse> ChangeAvatar(string email, string avatarLink)
        {
            if (email == null)
            {
                return new UserServiceResponse
                {
                    Succeeded = false,
                    Message = "недействительный адрес электронной почты"
                };
            }
            if (avatarLink == null)
            {
                return new UserServiceResponse
                {
                    Succeeded = false,
                    Message = "недействительная ссылка на изображение"
                };
            }
            MsuUser user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                user.AvatarImage = avatarLink;
                await _userManager.UpdateAsync(user);
                return new UserServiceResponse
                {
                    Succeeded = true,
                    Message = "Аватар успешно изменён"
                };
            }
            return new UserServiceResponse
            {
                Succeeded = false,
                Message = "Такого пользователя не существует"
            };
        }

        public async Task<IEnumerable<UserViewModel>> GetStudents()
        {
            var students = new List<UserViewModel>();
            foreach (var i in _userManager.Users)
            {
                if (!i.IsTeacher)
                {
                    var student = new UserViewModel
                    {
                        Id = i.Id,
                        UserName = i.UserName,
                        FatherName = i.FatherName,
                        FirstName = i.FirstName,
                        LastName = i.LastName,
                        StudentCardId = i.StudentCardId,
                        PhoneNumber = i.PhoneNumber,
                        GroupNumber = i.GroupNumber,
                        FacultyId = i.FacultyId,
                        Email = i.Email,
                        Roles = await _userManager.GetRolesAsync(i)
                    };
                    students.Add(student);
                }
            }
            return students;
        }

        public async Task<IEnumerable<UserViewModel>> GetTeachers()
        {
            var teachers = new List<UserViewModel>();
            foreach (var i in _userManager.Users)
            {
                if (i.IsTeacher)
                {
                    var teacher = new UserViewModel
                    {
                        Id = i.Id,
                        UserName = i.UserName,
                        FatherName = i.FatherName,
                        FirstName = i.FirstName,
                        LastName = i.LastName,
                        PhoneNumber = i.PhoneNumber,
                        GroupNumber = i.GroupNumber,
                        FacultyId = i.FacultyId,
                        Email = i.Email,
                        Roles = await _userManager.GetRolesAsync(i)
                    };
                    teachers.Add(teacher);
                }
            }
            return teachers;
        }

        public async Task<AuthenticationModel> GetTokenAsync(TokenRequest model)
        {
            var authenticationModel = new AuthenticationModel();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {model.Email}.";
                return authenticationModel;
            }
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authenticationModel.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.Email = user.Email;
                authenticationModel.IsTeacher = user.IsTeacher;
                authenticationModel.UserName = user.UserName;
                authenticationModel.AvatarImage = user.AvatarImage;
                authenticationModel.FirstName = user.FirstName;
                authenticationModel.LastName = user.LastName;
                authenticationModel.FatherName = user.FatherName;
                authenticationModel.FacultyId = user.FacultyId;
                authenticationModel.StudentCardId = user.StudentCardId;
                authenticationModel.GroupNumber = user.GroupNumber;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.ToList();
                return authenticationModel;
            }
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
            return authenticationModel;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(MsuUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        public async Task<UserServiceResponse> RegisterAsync(RegisterModel model)
        {
            var user = new MsuUser
            {
                AvatarImage = model.AvatarImage,
                Email = model.Email,
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FatherName = model.FatherName,
                GroupNumber = Convert.ToInt32(model.GroupNumber),
                StudentCardId = Convert.ToInt32(model.GroupNumber),
                FacultyId = Convert.ToInt32(model.FacultyId),
                IsTeacher = model.IsTeacher
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var emailConfirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodedEmailToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(user.Id + '&' + emailConfirmToken));

                await _userManager.AddToRoleAsync(user, Authorization.default_role.ToString());

                return new UserServiceResponse
                {
                    Succeeded = true,
                    User = user,
                    Message = "Успешно! Подтвердите почту!",
                    Code = encodedEmailToken
                };
            }
            return new UserServiceResponse
            {
                Succeeded = false,
                Message = $"{result.Errors.ToList().First().Description}"
            };
        }

        public async Task<UserServiceResponse> RequestPasswordResetAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(user.Id + '&' + resetPasswordToken));

            return new UserServiceResponse
            {
                Succeeded = true,
                User = user,
                Message = "Код выслан на почту",
                Code = code
            };
        }

        public async Task<UserServiceResponse> ResetPasswordAsync(string code, string password)
        {
            var decoded = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            var user = await _userManager.FindByIdAsync(decoded.Substring(0, decoded.IndexOf('&')));
            var token = decoded.Substring(decoded.IndexOf('&') + 1);

            var result = await _userManager.ResetPasswordAsync(user, token, password);
            if (result.Succeeded)
            {
                return new UserServiceResponse
                {
                    Succeeded = true,
                    Message = "Ваш пароль успешно изменён"
                };
            }

            return new UserServiceResponse
            {
                Succeeded = false,
                Message = result.Errors.ToList().First().Description
            };
        }

        public async Task<UserServiceResponse> RequestEmailChangeAsync(string password, string email, string newEmail)
        {
            MsuUser user = null;
            if (!string.IsNullOrEmpty(email))
            {
                user = await _userManager.FindByEmailAsync(email);
            }
            if (user == null)
            {
                return new UserServiceResponse
                {
                    Succeeded = false,
                    Message = "Такого пользователя не существует"
                };
            }

            if (await _userManager.CheckPasswordAsync(user, password))
            {
                var changeEmailToken = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
                var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(user.Id + '&' + newEmail + '&' + changeEmailToken));
                return new UserServiceResponse
                {
                    Succeeded = true,
                    User = user,
                    Message = "Код выслан на почту",
                    Code = code
                };
            }
            return new UserServiceResponse
            {
                Succeeded = false,
                Message = "Неверный пароль",
            };
        }

        public async Task<UserServiceResponse> ChangeEmailAsync(string code)
        {
            var decoded = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            var data = decoded.Split('&');
            var user = await _userManager.FindByIdAsync(data[0]);
            var newEmail = data[1];
            var token = data[2];

            var result = await _userManager.ChangeEmailAsync(user, newEmail, token);
            if (result.Succeeded)
            {
                return new UserServiceResponse
                {
                    Succeeded = true,
                    Message = "Ваш адрес электронной почты был успешно изменён!"
                };
            }

            return new UserServiceResponse
            {
                Succeeded = false,
                Message = result.Errors.ToList().First().Description
            };
        }

        public async Task<UserServiceResponse> VerifyEmailAsync(string code)
        {
            var decoded = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            var user = await _userManager.FindByIdAsync(decoded.Substring(0, decoded.IndexOf('&')));
            var token = decoded.Substring(decoded.IndexOf('&') + 1);

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return new UserServiceResponse
                {
                    Succeeded = true,
                    Message = "Почта успешно подтверждена"
                };
            }

            return new UserServiceResponse
            {
                Succeeded = false,
                Message = result.Errors.ToList().First().Description
            };
        }

        public async Task<IEnumerable<string>> GetRoles()
        {
            return await _roleManager.Roles.Select(i => i.Name).ToListAsync();
        }

        public async Task<UserServiceResponse> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            var role = await _roleManager.FindByNameAsync(model.Role);
            if (user == null)
            {
                return new UserServiceResponse
                {
                    Succeeded = false,
                    Message = "Такого пользователя не существует"
                };
            }
            if (role == null)
            {
                return new UserServiceResponse
                {
                    Succeeded = false,
                    Message = "Такой роли не существует"
                };
            }
            await _userManager.AddToRoleAsync(user, role.ToString());
            return new UserServiceResponse
            {
                Succeeded = true,
                Message = $"Роль {role.ToString()} успешно добавлена аккаунту {user.UserName}"
            };
        }

    }
}
