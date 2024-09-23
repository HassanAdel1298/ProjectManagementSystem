using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Application.ViewModel.RabbitMQMessages
{
    public class ProjectCreateMessage : BaseMessage
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public int UserCreateID { get; set; }
    }
}
