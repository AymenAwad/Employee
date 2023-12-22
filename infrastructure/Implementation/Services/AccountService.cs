using Application.CQFeatcture.Employees.Commands;
using Application.Dtos.AuthDto;
using Application.Dtos.EmployeeDto;
using Application.Features.IdentityFeatures.ApplicationRoleFeatures.Commands;
using Application.Features.IdentityFeatures.UserApplicationRole.Commands;
using Application.Interface.Interfaces;
using Application.Interface.Services;
using Common.Services;
using Domain.Auth;
using Domain.Common;
using Domain.Entities.Identity;
using Domain.Entities.Application;
using Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using Microsoft.IdentityModel.Tokens;
using Persistence.Contexts;
using Shared.DTOs.Email;
using Shared.Exceptions;
using Shared.Globalization;
using Shared.Interfaces.Services;
using Shared.Services;
using Shared.Settings;
using Shared.Wrappers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;
        private readonly IFeatureManager _featureManager;
        private readonly IEmployee _employee;
        private readonly IMediator _mediator;
        private readonly IApplicationDbContext _context;
        public AccountService(UserManager<ApplicationUser> userManager,
           RoleManager<Role> roleManager,
           IOptions<JWTSettings> jwtSettings,
           IDateTimeService dateTimeService,
           SignInManager<ApplicationUser> signInManager,
           IEmailService emailService,
           IFeatureManager featureManager,
           IEmployee employee,
           IMediator mediator,
           IApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
            _signInManager = signInManager;
            _emailService = emailService;
            _featureManager = featureManager;
            _employee = employee;
            _mediator = mediator;
            _context = context;
        }
        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new ApiException($"No Accounts Registered with {request.Email}.");
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new ApiException($"Invalid Credentials for '{request.Email}'.");
            }
            if (!user.EmailConfirmed)
            {
                throw new ApiException($"Account Not Confirmed for '{request.Email}'.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);

            List<RolesAndPermissionsOrganizationUserDto> rolesAndPermissionsOrganizations = new List<RolesAndPermissionsOrganizationUserDto>();
            AuthenticationResponse response = new AuthenticationResponse();
            Employee employee = new Employee();
            response.Id = user.Id;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.UserTypeId = user.UserTypeId;
            if (int.Parse(user.UserTypeId) == (int)UserTypeEnum.Employee)
            {
                employee = _context.Employees.FirstOrDefault(x => x.ApplicationUserId == user.Id);

                if (employee.Id > 0)
                {
                    response.ApplicationId = employee.Id;
                    response.ApplicationName = employee.Name;
                }
            }

            List<string> roleName = new List<string>();


            roleName.Add((int.Parse(user.UserTypeId) == (int)UserTypeEnum.Employee) ?
                            Enum.GetName(typeof(Domain.Enum.Roles), 1) :
                            "Not Found Role");  //rolesList.ToList();

            response.Roles = roleName;
            response.IsVerified = user.EmailConfirmed;
            var refreshToken = GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;
            return new Response<AuthenticationResponse>(response, $"Authenticated {user.UserName}");
        }
        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                throw new ApiException($"Username '{request.UserName}' is already taken.");
            }
            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.UserName,
                UserTypeId = request.UserTypeId,
                ActivationCode = GenerateRandomNo().ToString(),
                CreateBy = request.CreateBy,
                CreationDate = DateTime.Now,
                Status = request.Status,
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());

                    var verificationUri = await SendVerificationEmail(user, origin);

                    if (await _featureManager.IsEnabledAsync(nameof(FeatureManagement.EnableEmailService)))
                    {
                        var emailRequest = new EmailRequest()
                        {
                            From = "RowadAlnaql@tga.gov.sa",
                            To = user.Email,
                            Body = $"Please confirm your account by visiting this URL {verificationUri}",
                            Subject = "Confirm Registration"
                        };
                        await _emailService.SendAsync(emailRequest);

                    }
                    return new Response<string>(user.Id, message: $"User Registered. Please confirm your account by visiting this URL {verificationUri}");
                }
                else
                {
                    throw new ApiException($"{result.Errors.ToList()[0].Description}");
                }
            }
            else
            {
                throw new ApiException($"Email {request.Email} is already registered.");
            }
        }

        private async Task<string> SendVerificationEmail(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            //var route = "api/account/confirm-email/";
            var route = "api/v.01/confirm-emails/";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            //Email Service Call Here
            return verificationUri;
        }
        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),

                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }
        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
        private int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        public async Task<Response<string>> RegisterEmployeeAsync(RegisterEmployeeDto request, string origin)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(request.RegisterRequest.UserName);
            if (userWithSameUserName != null)
            {
                throw new ApiException($"Username '{request.RegisterRequest.UserName}' is already taken.");
            }
            var user = new ApplicationUser
            {
                Email = request.RegisterRequest.Email,
                UserName = request.RegisterRequest.UserName,
                UserTypeId = request.RegisterRequest.UserTypeId,
                ActivationCode = GenerateRandomNo().ToString(),
                CreateBy = request.RegisterRequest.CreateBy,
                CreationDate = DateTime.Now,
                Status = request.RegisterRequest.Status,
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.RegisterRequest.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.RegisterRequest.Password);

                if (result.Succeeded)
                {

                    if (int.Parse(request.RegisterRequest.UserTypeId) == (int)UserTypeEnum.Employee)
                    {
                        Response<int> organization = await _mediator.Send(new CreateEmployeeCommand()
                        {
                            Agency = request.CreateEmployee.Agency,
                            Name =  request.CreateEmployee.EmployeeName,
                            Department = request.CreateEmployee.Department,
                            Rating = request.CreateEmployee.Rating,
                            Year = request.CreateEmployee.Year,
                            Category = request.CreateEmployee.Category,
                            Status = request.CreateEmployee.Status,
                            ApplicationUserId = user.Id,
                        });

                        if (organization.Data > 0)
                        {
                            #region Role
                            Response<int> userApplicationRole = await _mediator.Send(new CreateApplicationRoleCommand()
                            {
                                RoleId = "7642c6c6-69d8-4ce3-9e8e-af5300a116cb",
                                ApplicationTypeId = (int)UserTypeEnum.Employee,
                                ApplicationId = organization.Data,
                            });

                            Response<int> userApplicationRolex = await _mediator.Send(new CreateUserApplicationRoleCommand()
                            {
                                ApplicationUserId = user.Id,
                                ApplicationRoleId = userApplicationRole.Data,
                            });
                            #endregion

                        }
                    }
                }
            }
            return new Response<string>(user.Id, message: $"User Registered");
        }
        public async Task<Response<string>> ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return new Response<string>(user.Id, message: $"Account Confirmed for {user.Email}. You can now use the /api/Account/login endpoint.");
            }
            else
            {
                throw new ApiException($"An error occured while confirming {user.Email}.");
            }
        }

        public async Task<Response<string>> ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var account = await _userManager.FindByEmailAsync(model.Email);

            // always return ok response to prevent email enumeration
            if (account == null) return new Response<string>(model.Email, message: $"not found account.");

            await ReSendVerificationActivationCode(account.Id, origin);

            return new Response<string>(HttpStatusCode.OK, account.Id, message: $"Seccessfully.");
        }

        public async Task<Response<string>> ResetPassword(ResetPasswordRequest model)
        {
            var account = await _userManager.FindByEmailAsync(model.Email);

            if (account == null) throw new ApiException($"No Accounts Registered with {model.Email}.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(account);

            var result = await _userManager.ResetPasswordAsync(account, token, model.Password);

            if (result.Succeeded)
            {
                return new Response<string>(HttpStatusCode.OK, account.Id, message: $"Seccessfully Password Resetted.");
            }
            else
            {
                return new Response<string>(HttpStatusCode.OK, account.Id, message: $"Error occured while reseting the password.");

            }
        }

        public async Task<Response<string>> RefreshToSendVerificationActivationCode(string userId, string origin)
        {

            string code = GenerateRandomNo().ToString();

            ApplicationUser? user = await _userManager.Users.Where(x => x.Id == userId).Select(x => new ApplicationUser
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                FullName = x.FullName,
                ProfileImage = x.ProfileImage,
                UserTypeId = x.UserTypeId,
                LastChangePassword = x.LastChangePassword,
                LastLoginDateTime = x.LastLoginDateTime,
                LastLoginStatus = x.LastLoginStatus,
                ActivationCode = code,
                IsLogedIn = x.IsLogedIn,
                CreateBy = x.CreateBy,
                CreationDate = x.CreationDate,
                ModifiedBy = x.ModifiedBy,
                LastUpdateDate = DateTime.UtcNow,
                Status = x.Status, //(int)StatusEnum.Active,
                UserName = x.UserName,
                NormalizedUserName = x.NormalizedUserName,
                Email = x.Email,
                NormalizedEmail = x.NormalizedEmail,
                EmailConfirmed = x.EmailConfirmed,
                PasswordHash = x.PasswordHash,
                SecurityStamp = x.SecurityStamp,
                ConcurrencyStamp = x.ConcurrencyStamp,
                PhoneNumber = x.PhoneNumber,
                PhoneNumberConfirmed = x.PhoneNumberConfirmed,
                TwoFactorEnabled = x.TwoFactorEnabled,
                LockoutEnd = x.LockoutEnd,
                LockoutEnabled = x.LockoutEnabled,
                AccessFailedCount = x.AccessFailedCount
            }).FirstOrDefaultAsync();

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                //Email Service Call Here
                var emailRequest = new EmailRequest()
                {
                    //Body = $"Please verification activate code by visiting this URL - {verificationUri}",
                    Body = $"Please verification activate code - {code}",
                    To = user.Email,
                    Subject = "Verification Activate Code",
                };
                await _emailService.SendAsync(emailRequest);

                return new Response<string>(HttpStatusCode.OK, user.Id, message: $"Seccessfully renew send activate code {code}.");
            }
            else
            {
                return new Response<string>(HttpStatusCode.BadRequest, user.Id, message: $"bad request renew send activate code.");
            }

        }

        public async Task<string> RefreshToSendVerificationPhoneNumber(string userId, string origin)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
            var route = "api/v.01/verification-phone-numbers/";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            //Email Service Call Here
            return verificationUri;
        }


        public async Task<Response<string>> ConfirmActivationCodeAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return new Response<string>(HttpStatusCode.OK, user.Id, message: $"Your Account {user.UserName} is Confirm Activated Code Seccessfully.");
            }
            else
            {
                throw new ApiException($"An error occured while Activated.");
            }
        }

        public async Task<Response<string>> ConfirmPhoneNumberAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _userManager.ChangePhoneNumberAsync(user, code, user.PhoneNumber);

            if (result.Succeeded)
            {
                return new Response<string>(user.Id, message: $"Phone number confirmed.");
            }
            else
            {
                throw new ApiException($"An error occured while confirming {user.PhoneNumber}.");
            }
        }

        private async Task<Response<string>> ReSendVerificationActivationCode(string userId, string origin)
        {
            string code = GenerateRandomNo().ToString();

            ApplicationUser? user = await _userManager.Users.Where(x => x.Id == userId).Select(x => new ApplicationUser
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                FullName = x.FullName,
                ProfileImage = x.ProfileImage,
                UserTypeId = x.UserTypeId,
                LastChangePassword = x.LastChangePassword,
                LastLoginDateTime = x.LastLoginDateTime,
                LastLoginStatus = x.LastLoginStatus,
                ActivationCode = code,
                IsLogedIn = x.IsLogedIn,
                CreateBy = x.CreateBy,
                CreationDate = x.CreationDate,
                ModifiedBy = x.ModifiedBy,
                LastUpdateDate = DateTime.UtcNow,
                Status = x.Status, //(int)StatusEnum.Active,
                UserName = x.UserName,
                NormalizedUserName = x.NormalizedUserName,
                Email = x.Email,
                NormalizedEmail = x.NormalizedEmail,
                EmailConfirmed = x.EmailConfirmed,
                PasswordHash = x.PasswordHash,
                SecurityStamp = x.SecurityStamp,
                ConcurrencyStamp = x.ConcurrencyStamp,
                PhoneNumber = x.PhoneNumber,
                PhoneNumberConfirmed = x.PhoneNumberConfirmed,
                TwoFactorEnabled = x.TwoFactorEnabled,
                LockoutEnd = x.LockoutEnd,
                LockoutEnabled = x.LockoutEnabled,
                AccessFailedCount = x.AccessFailedCount
            }).FirstOrDefaultAsync();

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                //Email Service Call Here
                var emailRequest = new EmailRequest()
                {
                    //Body = $"Please verification activate code by visiting this URL - {verificationUri}",
                    Body = $"Please verification activate code - {code}",
                    To = user.Email,
                    Subject = "Verification Activate Code",
                };
                await _emailService.SendAsync(emailRequest);

                return new Response<string>(HttpStatusCode.OK, user.Id, message: $"Seccessfully renew send activate code {code}.");
            }
            else
            {
                return new Response<string>(HttpStatusCode.BadRequest, user.Id, message: $"bad request renew send activate code.");
            }
        }
    }
}