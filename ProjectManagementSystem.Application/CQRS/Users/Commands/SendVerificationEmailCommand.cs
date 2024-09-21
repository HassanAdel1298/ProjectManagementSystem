using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ProjectManagementSystem.Application.DTO;
using Microsoft.Extensions.Options;
using ProjectManagementSystem.Application.DTO.Users;

namespace ProjectManagementSystem.Application.CQRS.Users.Commands
{
    public record SendVerificationEmailCommand(SendEmailDTO SendEmailDTO) : IRequest<ResultDTO<bool>>;


    public class SendVerificationEmailCommandHendler : IRequestHandler<SendVerificationEmailCommand, ResultDTO<bool>>
    {

        public async Task<ResultDTO<bool>> Handle(SendVerificationEmailCommand request, CancellationToken cancellationToken)
        {

            var senderEmail = "hassan.adel1298@gmail.com";
            var senderPassword = "voxg zefr snvb zvsr";


            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true

            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = request.SendEmailDTO.Subject,
                Body = request.SendEmailDTO.Body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(request.SendEmailDTO.ToEmail);

            await client.SendMailAsync(mailMessage);

            return ResultDTO<bool>.Sucess(true, "Send Email Successfully!");

        }
    }
}
