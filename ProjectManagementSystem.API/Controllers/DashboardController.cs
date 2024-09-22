using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Application.CQRS.Dashboard.Orchestrators;
using ProjectManagementSystem.Application.CQRS.Tasks.Queries;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.DTO.Dashboard;
using ProjectManagementSystem.Application.DTO.Tasks;
using ProjectManagementSystem.Application.ViewModel;

namespace ProjectManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : BaseController
    {
        public DashboardController(ControllereParameters controllereParameters) : base(controllereParameters)
        {
        }


        

        [HttpGet]
        public async Task<ResultViewModel<DashboardDTO>> GetDashboard()
        {
            

            var resultDTO = await _mediator.Send(new GetDashboardOrchestrator());

            if (!resultDTO.IsSuccess)
            {
                return ResultViewModel<DashboardDTO>.Faliure(resultDTO.Message);
            }

            return ResultViewModel<DashboardDTO>.Sucess(resultDTO.Data, resultDTO.Message);
        }


    }
}
