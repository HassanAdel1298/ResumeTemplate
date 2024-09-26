using MediatR;
using ResumeTemplate.CQRS.Users.Commands;
using ResumeTemplate.DTO;
using ResumeTemplate.DTO.Users;
using ResumeTemplate.Entities;
using ResumeTemplate.Helpers;


namespace ResumeTemplate.CQRS.Users.Orchestrators
{
    public record RegisterUserOrchestrator(RegisterUserDTO registerUserDTO) : IRequest<ResultDTO<RegisterUserDTO>>;

    public class RegisterUserOrchestratorHandler : BaseRequestHandler<User ,RegisterUserOrchestrator, ResultDTO<RegisterUserDTO>>
    {
        public RegisterUserOrchestratorHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<RegisterUserDTO>> Handle(RegisterUserOrchestrator request, CancellationToken cancellationToken)
        {
            var resultRegisterUserDTO = await _mediator.Send(new RegisterUserCommand(request.registerUserDTO));

            if (!resultRegisterUserDTO.IsSuccess)
            {
                return resultRegisterUserDTO;
            }


            SendEmailDTO sendEmailDTO = new SendEmailDTO()
            {
                ToEmail = resultRegisterUserDTO.Data.Email,
                Subject = "Verify your email",
                Body = $"Please verify your email address by OTP : {resultRegisterUserDTO.Data.OTP}"
            };
    

            await _mediator.Send(new SendVerificationEmailCommand(sendEmailDTO));


            return resultRegisterUserDTO;
        }
    }
}
