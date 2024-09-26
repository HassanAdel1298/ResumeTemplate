using MediatR;
using System.Net.Mail;
using System.Net;
using ResumeTemplate.DTO.Users;
using ResumeTemplate.DTO;


namespace ResumeTemplate.CQRS.Users.Commands
{
    public record SendVerificationEmailCommand(SendEmailDTO SendEmailDTO) : IRequest<ResultDTO<bool>>;


    public class SendVerificationEmailCommandHendler : IRequestHandler<SendVerificationEmailCommand, ResultDTO<bool>>
    {

        public async Task<ResultDTO<bool>> Handle(SendVerificationEmailCommand request, CancellationToken cancellationToken)
        {

            var senderEmail = "hassan.adel1298@gmail.com";
            var senderPassword = "orzq gtsv qttq bfyv";


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
