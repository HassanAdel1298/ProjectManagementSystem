using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Application.CQRS.Tasks.Queries;
using ProjectManagementSystem.Application.CQRS.Users.Commands;
using ProjectManagementSystem.Application.CQRS.Users.Orchestrators;
using ProjectManagementSystem.Application.CQRS.Users.Queries;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.DTO.Tasks;
using ProjectManagementSystem.Application.DTO.Users;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Application.ViewModel;
using ProjectManagementSystem.Application.ViewModel.Tasks;
using ProjectManagementSystem.Application.ViewModel.Users;

namespace ProjectManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseController
    {

        public UserController(ControllereParameters controllereParameters) : base(controllereParameters)
        {
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
        [Authorize]
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
        [Authorize]
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


        [HttpGet]
        [Authorize]
        public async Task<ResultViewModel<IEnumerable<UsersReturnViewDTO>>> GetAllUsers(ViewUsersViewModel viewUsersViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<IEnumerable<UsersReturnViewDTO>>.Faliure("User isn't Login");
            }

            var userViewDTO = viewUsersViewModel.MapOne<UsersViewDTO>();

            var resultDTO = await _mediator.Send(new ViewAllUsersQuery(userViewDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<IEnumerable<UsersReturnViewDTO>>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<IEnumerable<UsersReturnViewDTO>>.Sucess(resultDTO.Data, resultDTO.Message);
        }


        [HttpPost]
        [Authorize]
        public async Task<ResultViewModel<IEnumerable<UsersReturnViewDTO>>> SearchUsersByName(UsersSearchViewModel usersSearchViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<IEnumerable<UsersReturnViewDTO>>.Faliure("User isn't Login");
            }

            var userSearchDTO = usersSearchViewModel.MapOne<UsersSearchDTO>();

            var resultDTO = await _mediator.Send(new SearchUsersByNameQuery(userSearchDTO));

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<IEnumerable<UsersReturnViewDTO>>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<IEnumerable<UsersReturnViewDTO>>.Sucess(resultDTO.Data, resultDTO.Message);
        }

    }
}
