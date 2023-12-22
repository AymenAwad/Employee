using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Application.Features.IdentityFeatures.ApplicationRoleFeatures.Commands;
using Application.Features.IdentityFeatures.ApplicationRoleFeatures.Queries;

using Application.Features.IdentityFeatures.RoleFeatures.Commands;
using Application.Features.IdentityFeatures.RoleFeatures.Queries;
using Application.Features.IdentityFeatures.UserApplicationRole.Commands;
using Domain.Auth;
using Infrastructure.Extension;
using Shared.DTOs.Email;
using Shared.Interfaces.Services;
using Application.Interface.Services;
using Application.Dtos.AuthDto;
using Employee.WebApi.Controllers;

namespace Api.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}")]
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;
        private readonly IEmailService _email;

        public AccountController(IAccountService accountService, IEmailService email)
        {
            _accountService = accountService;
            _email = email;
        }

        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateAsync(request, GenerateIPAddress()));
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterAsync(request, origin));
        }

        [HttpPost("register-employee")]
        public async Task<IActionResult> RegisterEmployeeAsync(RegisterEmployeeDto request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterEmployeeAsync(request, origin));
        }

        [HttpGet("confirm-emails")]
        public async Task<IActionResult> ConfirmEmailAsync([FromQuery] string userId, [FromQuery] string code)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.ConfirmEmailAsync(userId, code));
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            //Request.Headers["origin"]            
            return Ok(await _accountService.ForgotPassword(model, ApiRoutes.BaseUrl));
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest model)
        {

            return Ok(await _accountService.ResetPassword(model));
        }

        //=================
        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            return Ok(await Mediator.Send(new GetAllRolesQuery()));
        }

        [HttpGet("roles/{roleId}")]
        public async Task<IActionResult> GetRoleById(string roleId)
        {
            return Ok(await Mediator.Send(new GetRoleByIdQuery() { Id = roleId }));
        }

        [HttpPost("roles")]
        public async Task<IActionResult> CreateRole(CreateRoleCommand model)
        {
            return Ok(await Mediator.Send(model));
        }

        [HttpDelete("roles/{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            return Ok(await Mediator.Send(new DeleteRoleCommand() { Id = roleId }));
        }


        [HttpGet("application-roles")]
        public async Task<IActionResult> GetAllApplicationRoles(int applicationId, [Required] int applicationTypeId)
        {
            return Ok(await Mediator.Send(new GetAllApplicationRolesQuery() { ApplicationId = applicationId, ApplicationTypeId = applicationTypeId }));
        }

        [HttpGet("application-roles/{applicationRoleId}")]
        public async Task<IActionResult> GetApplicationRoleById(int applicationRoleId, [Required] int applicationTypeId, [Required] int applicationId)
        {
            return Ok(await Mediator.Send(new GetApplicationRoleByIdQuery() { Id = applicationRoleId, ApplicationTypeId = applicationTypeId, ApplicationId = applicationId }));
        }

        [HttpPost("application-roles")]
        public async Task<IActionResult> CreateApplicationRole(CreateApplicationRoleCommand model)
        {
            return Ok(await Mediator.Send(model));
        }

        [HttpDelete("application-roles/{applicationRoleId}")]
        public async Task<IActionResult> DeleteApplicationRole(int applicationRoleId, DeleteApplicationRoleCommand model)
        {
            model.Id = applicationRoleId;
            return Ok(await Mediator.Send(model));
        }


        [HttpGet("verification-activate-cods")]
        public async Task<IActionResult> ConfirmActivationCodeAsync(string userId, string code)
        {
            return Ok(await _accountService.ConfirmActivationCodeAsync(userId, code));
        }

        [HttpGet("verification-phone-numbers")]
        public async Task<IActionResult> ConfirmPhoneNumberAsync([FromQuery] string userId, [FromQuery] string code)
        {
            return Ok(await _accountService.ConfirmPhoneNumberAsync(userId, code));
        }

        [HttpGet("refresh-to-send-verification-activate-cods")]
        public async Task<IActionResult> RefreshToSendVerificationActivationCode([FromQuery] string userId)
        {
            return Ok(await _accountService.RefreshToSendVerificationActivationCode(userId, ApiRoutes.BaseUrl));
        }

        [HttpGet("refresh-to-send-verification-phone-numbers")]
        public async Task<IActionResult> RefreshToSendVerificationPhoneNumber([FromQuery] string userId)
        {
            return Ok(await _accountService.RefreshToSendVerificationPhoneNumber(userId, ApiRoutes.BaseUrl));
        }

        [HttpPost("Email")]
        public async Task<IActionResult> Send(EmailRequest email)
        {

            await _email.SendAsync(email);
            return Ok();
        }

        //=======
        [HttpPost("create-user-application-roles")]
        public async Task<IActionResult> CreateUserApplicationRole([Required] string applicationUserId, [Required] int applicationRoleId)
        {
            return Ok(await Mediator.Send(new CreateUserApplicationRoleCommand()
            {
                ApplicationUserId = applicationUserId,
                ApplicationRoleId = applicationRoleId,
            }));
        }

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
