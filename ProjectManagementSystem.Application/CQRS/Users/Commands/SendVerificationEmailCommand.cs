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

namespace ProjectManagementSystem.Application.CQRS.Users.Commands
{
    public record SendVerificationEmailCommand(SendEmailDTO SendEmailDTO) : IRequest<ResultDTO<bool>>;

    public class SendEmailDTO
    {
        public string ToEmail {  get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class SmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SendVerificationEmailCommandHendler : IRequestHandler<SendVerificationEmailCommand, ResultDTO<bool>>
    {
        private readonly SmtpSettings _smtpSettings;

        public SendVerificationEmailCommandHendler(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task<ResultDTO<bool>> Handle(SendVerificationEmailCommand request, CancellationToken cancellationToken)
        {

            var senderEmail = _smtpSettings.Email;
            var senderPassword = _smtpSettings.Password;


            var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
            {
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = _smtpSettings.EnableSsl

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
