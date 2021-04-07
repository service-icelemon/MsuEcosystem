using Application.Interfaces;
using Domain.Entitties.Identity;
using Domain.Entitties.Identity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public UserController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpGet("GetUserById")]
        public async Task<UserViewModel> GetUserById(string id)
        {
            return await _userService.GetUserById(id);
        }

        [HttpGet("GetStudents")]
        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            return await _userService.GetStudents();
        }

        [HttpGet("GetTeachers")]
        public async Task<IEnumerable<UserViewModel>> GetTeachers()
        {
            return await _userService.GetTeachers();
        }

        [HttpPost("ChangeAvatar")]
        public async Task<IActionResult> ChangeAvatar(string email, string avatarLink)
        {
            var result = await _userService.ChangeAvatar(email, avatarLink);
            if (result.Succeeded)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync(TokenRequest model)
        {
            var result = await _userService.GetTokenAsync(model);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterModel model)
        {
            var result = await _userService.RegisterAsync(model);

            if (result.Succeeded)
            {
                await _emailService.SendAsync(result.User.Email, "Подтверждение регистрации на msu.by", $"<h4>{result.Code}</h4>", true);
            }

            return Ok(result.Message);
        }

        [HttpPost("RequestEmailChange")]
        public async Task<ActionResult> EmailChangeRequestAsync(string password, string email, string newEmail)
        {
            var result = await _userService.RequestEmailChangeAsync(password, email, newEmail );
            if (result.Succeeded)
            {
                await _emailService.SendAsync(newEmail, "Изменение адреса электронной почты", $"{result.Code}", true);
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("ChangeEmail")]
        public async Task<ActionResult> EmailChangeAsync(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Неверные данные");
            }
            var result = await _userService.ChangeEmailAsync(code);
            if (result.Succeeded)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("RequestPasswordReset")]
        public async Task<ActionResult> ResetPasswordRequestAsync(string email)
        {
            var result = await _userService.RequestPasswordResetAsync(email);
            if (result.Succeeded)
            {
                await _emailService.SendAsync(result.User.Email, "Смена пароля на msu.by", $"{result.Code}", true);
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPasswordAsync(string code, string newPassword)
        {
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(newPassword))
            {
                return BadRequest("Неверные данные");
            }
            var result = await _userService.ResetPasswordAsync(code, newPassword);
            if (result.Succeeded)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("VerifyEmail")]
        public async Task<IActionResult> VerifyEmailAsync(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return NotFound();
            }

            var result = await _userService.VerifyEmailAsync(code);
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result);
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
        {
            var result = await _userService.AddRoleAsync(model);
            return Ok(result);
        }

        [HttpGet("GetRoleList")]
        public async Task<IEnumerable<string>> GetRoles()
        {
            return await _userService.GetRoles();
        }
    }
}
