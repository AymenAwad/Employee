using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interfaces.Services;

namespace Employee.WebApi.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private readonly IAuthenticatedUserService _authenticatedUser;
        private IWebHostEnvironment _webHostEnvironment;
        public BaseApiController() { }

        public BaseApiController(IAuthenticatedUserService authenticatedUser)
        {
            _authenticatedUser = authenticatedUser;
        }

        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        public string CurrentUserId;
        //public string CurrentUserId => HttpContext.User.FindFirst("uid").Value;
        protected IWebHostEnvironment WebHostEnvironment => _webHostEnvironment ??= HttpContext.RequestServices.GetService<IWebHostEnvironment>();

    }
}
