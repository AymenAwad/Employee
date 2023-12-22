using Domain.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Wrappers;
using Application.Dtos.EmployeeDto;
using Application.Dtos.AuthDto;

namespace Application.Interface.Services
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
        Task<Response<string>> ConfirmEmailAsync(string userId, string code);
        //Task ForgotPassword(ForgotPasswordRequest model, string origin);
        Task<Response<string>> ForgotPassword(ForgotPasswordRequest model, string origin);
        Task<Response<string>> ResetPassword(ResetPasswordRequest model);
        Task<Response<string>> RefreshToSendVerificationActivationCode(string userId, string origin);
        Task<string> RefreshToSendVerificationPhoneNumber(string userId, string origin);
        Task<Response<string>> ConfirmActivationCodeAsync(string userId, string code);
        Task<Response<string>> ConfirmPhoneNumberAsync(string userId, string code);

        Task<Response<string>> RegisterEmployeeAsync(RegisterEmployeeDto request, string origin);
    }
}
