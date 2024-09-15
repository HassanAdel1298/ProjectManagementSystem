using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Application.CQRS.Users.Commands;
using ProjectManagementSystem.Application.CQRS.Users.Orchestrators;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Application.ViewModel;
using ProjectManagementSystem.Application.ViewModel.Users;

namespace ProjectManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<ResultViewModel<RegisterUserDTO>> Register(RegisterViewModel registerViewModel)
        {
            var registerUserDTO = registerViewModel.MapOne<RegisterUserDTO>();

            var resultDTO = await _mediator.Send(new RegisterUserOrchestrator(registerUserDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<RegisterUserDTO>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<RegisterUserDTO>.Sucess(resultDTO.Data, resultDTO.Message);
        }

        [HttpPost]
        public async Task<ResultViewModel<string>> Login(LoginViewModel loginViewModel)
        {
            var loginUserDTO = loginViewModel.MapOne<LoginUserDTO>();

            var resultDTO = await _mediator.Send(new LoginUserCommand(loginUserDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<string>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<string>.Sucess(resultDTO.Data, resultDTO.Message);
        }


        [HttpPost]
        public async Task<ResultViewModel<bool>> VerifyAccount(VerifyAccountViewModel verifyAccountViewModel)
        {
            var verifyAccountDTO = verifyAccountViewModel.MapOne<VerifyUserDTO>();

            var resultDTO = await _mediator.Send(new VerifyAccountCommand(verifyAccountDTO));
            
            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<bool>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<bool>.Sucess(resultDTO.Data, resultDTO.Message);
        }


        [HttpPost]
        public async Task<ResultViewModel<bool>> ChangePassword(string Email)
        {
            

            var resultDTO = await _mediator.Send(new ChangePasswordOrchestrator(Email));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<bool>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<bool>.Sucess(true, resultDTO.Message);
        }


        [HttpPost]
        public async Task<ResultViewModel<bool>> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {

            var resetPasswordDTO = resetPasswordViewModel.MapOne<ResetPasswordDto>();

            var resultDTO = await _mediator.Send(new ResetPasswordCommand(resetPasswordDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<bool>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<bool>.Sucess(true, resultDTO.Message);
        }

    }
}
