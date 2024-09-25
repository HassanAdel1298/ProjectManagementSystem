using MediatR;
using ProjectManagementSystem.Application.CQRS.Users.Commands;
using ProjectManagementSystem.Application.DTO.Users;
using ProjectManagementSystem.Application.ViewModel.RabbitMQMessages;

namespace ProjectManagementSystem.API.Services.RabbitMQServices
{
    public class SendEmailConsumer : IBaseConsumer<SendEmailMessage>
    {
        IMediator _mediator;
        public SendEmailConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Task> Consume(SendEmailMessage message)
        {
            SendEmailDTO sendEmailDTO = new SendEmailDTO()
            {
                ToEmail = message.ToEmail,
                Subject = message.Subject,
                Body = message.Body
            };
            var sendVerificationEmailCommand = await _mediator.Send(new SendVerificationEmailCommand(sendEmailDTO));

            return Task.CompletedTask;
        }

    }
}
