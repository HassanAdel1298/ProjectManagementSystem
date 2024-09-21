using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Application.DTO;
using System.Security.Claims;

namespace ProjectManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        protected readonly UserState _userState;
        public BaseController(ControllereParameters controllereParameters)
        {
            _mediator = controllereParameters.Mediator;
            _userState = controllereParameters.UserState;

            var loggedUser = new HttpContextAccessor().HttpContext.User;

            
            _userState.ID = loggedUser?.FindFirst("UserID")?.Value ?? "";
            _userState.Email = loggedUser?.FindFirst(ClaimTypes.Email)?.Value ?? "";
            _userState.Name = loggedUser?.FindFirst(ClaimTypes.Name)?.Value ?? "";
        }
    }
}
