using Application.Services.UserService.Commands;
using Application.Services.UserService.Queries;
using Domain.Entitties.Identity;
using Domain.Entitties.Identity.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("loaduserdata")]
        public async Task<IActionResult> LoadUserData()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest("ошибка");
            }
            var currentUserData = await _mediator.Send(new GetUserData.Query(User));
            if (currentUserData != null)
            {
                return Ok(currentUserData.User);
            }
            return BadRequest("ошибка");
        }

        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("ошибка");
            }
            var result = await _mediator.Send(new UserRefreshToken.Command(refreshToken));
            if (result.Succeeded)
            {
                return Ok(result.Tokens);
            }
            return BadRequest("не удалось обновить токен");
        }

        [HttpGet("getuser")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _mediator.Send(new GetUser.Query(id));
            return result.Succeeded ? Ok(result.User) : BadRequest(result.Message);
        }

        [HttpGet("list")]
        public async Task<IEnumerable<UserViewModel>> GetUsers(Expression<Func<MsuUser, bool>> expression)
        {
            return await _mediator.Send(new GetUsersList.Query(expression));
        }

        [HttpPost("setavatar")]
        public async Task<IActionResult> ChangeAvatar(string avatarLink)
        {
            var currentUser = await _mediator.Send(new GetCurrentUser.Query(User));
            var result = await _mediator.Send(new UserSetAvatar.Command(currentUser.User, avatarLink));
            return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpPost("gettoken")]
        public async Task<IActionResult> GetTokenAsync(string email, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Неверные данные");
            }
            var result = await _mediator.Send(new GetUserToken.Query(email, password));
            return result.Succeeded ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(UserRegisterModel model)
        {
            var result = await _mediator.Send(new RegisterUser.Command(model));
            return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpPost("changeemailrequest")]
        public async Task<ActionResult> EmailChangeRequestAsync(string email)
        {
            var currentUser = await _mediator.Send(new GetCurrentUser.Query(User));
            if (currentUser.User != null)
            {
                var result = await _mediator.Send(new UserEmailChangeRequest.Command(currentUser.User, email));
                return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
            }
            return BadRequest();
        }

        [HttpPost("changeemail")]
        public async Task<ActionResult> EmailChangeAsync(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Неверные данные");
            }
            var result = await _mediator.Send(new UserEmailChange.Command(code));
            return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpPost("requestpasswordreset")]
        public async Task<ActionResult> ResetPasswordRequestAsync(string email)
        {
            var result = await _mediator.Send(new UserResetPasswordRequest.Command(email));
            return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpPost("passwordreset")]
        public async Task<ActionResult> ResetPasswordAsync(string code, string newPassword)
        {
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(newPassword))
            {
                return BadRequest("Неверные данные");
            }
            var result = await _mediator.Send(new UserResetPassword.Command(code, newPassword));
            return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpGet("verifyemail")]
        public async Task<IActionResult> VerifyEmailAsync(string code)
        {
            var result = await _mediator.Send(new UserVerifyEmail.Command(code));
            return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpPost("role/add")]
        public async Task<IActionResult> AddRoleAsync(string userId, string roleName)
        {
            var result = await _mediator.Send(new UserAddRole.Command(userId, roleName));
            return result.Succeeded ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpGet("roles")]
        public async Task<IEnumerable<string>> GetRoles()
        {
            return await _mediator.Send(new GetRoleList.Query());
        }
    }
}
