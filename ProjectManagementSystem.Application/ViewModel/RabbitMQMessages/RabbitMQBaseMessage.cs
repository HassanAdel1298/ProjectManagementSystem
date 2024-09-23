namespace ProjectManagementSystem.Application.ViewModel.RabbitMQMessages
{
    public enum MessageType
    {
        User,
        Project,
        Task
    }

    public class BaseMessage
    {
        public DateTime SentDate { get; set; }

        public string Sender { get; set; }
        public string Action { get; set; }

        public virtual string Type { get; set; }
    }
}
