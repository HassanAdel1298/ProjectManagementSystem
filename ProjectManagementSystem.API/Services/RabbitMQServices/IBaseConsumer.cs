
using ProjectManagementSystem.Application.ViewModel.RabbitMQMessages;

namespace ProjectManagementSystem.API.Services.RabbitMQServices
{
    public interface IBaseConsumer<T> where T : BaseMessage
    {
        Task<Task> Consume(T message);
    }
}
