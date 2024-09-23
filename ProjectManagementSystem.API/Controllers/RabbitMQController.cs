using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementSystem.Application.DTO;
using ProjectManagementSystem.Application.ViewModel.Projects;
using ProjectManagementSystem.Application.ViewModel.RabbitMQMessages;
using ProjectManagementSystem.API.Services;
using ProjectManagementSystem.Application.DTO.Projects;
using ProjectManagementSystem.Application.Helpers;
using ProjectManagementSystem.Application.ViewModel;

namespace ProjectManagementSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RabbitMQController : BaseController
    {
        RabbitMQPublisherService _rabbitMQService;
        public RabbitMQController(ControllereParameters controllereParameters, RabbitMQPublisherService rabbitMQService) : base(controllereParameters)
        {
            _rabbitMQService = rabbitMQService;
        }

        [HttpPost]
        [Authorize]
        public ResultViewModel<bool> CreateProject(ProjectCreateViewModel projectViewModel)
        {
            int userID;
            bool isUserID = int.TryParse(_userState.ID, out userID);

            if (!isUserID)
            {
                return ResultViewModel<bool>.Faliure("User isn't Login");
            }


            
            ProjectCreateMessage baseMessage = new ProjectCreateMessage
            {
                Sender = "ProjectManagementSystem",
                Action = "CreateProject",
                SentDate = DateTime.Now,
                Title = projectViewModel.Title,
                Description = projectViewModel.Description,
                IsPublic = projectViewModel.IsPublic,
                UserCreateID = userID
            };

            baseMessage.Type = baseMessage.GetType().Name;

            string msg = Newtonsoft.Json.JsonConvert.SerializeObject(baseMessage);


            _rabbitMQService.PublishMessage(msg);

            return ResultViewModel<bool>.Sucess(true,"Send Message to RabbitMQ");

        }
    }
}
