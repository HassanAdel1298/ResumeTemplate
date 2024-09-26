using MediatR;
using ResumeTemplate.CQRS.Users.Commands;
using ResumeTemplate.DTO;
using ResumeTemplate.Entities;
using ResumeTemplate.Helpers;
using ResumeTemplate.DTO.Educations;
using ResumeTemplate.CQRS.Educations.Commands;

namespace ResumeTemplate.CQRS.Educations.Orchestrators
{
    public record CreateEducationOrchestrator(EducationCreateDTO educationDTO) : IRequest<ResultDTO<EducationCreateDTO>>;

    public class CreateEducationOrchestratorHandler : BaseRequestHandler<Experience, CreateEducationOrchestrator, ResultDTO<EducationCreateDTO>>
    {
        public CreateEducationOrchestratorHandler(RequestParameters<Experience> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<EducationCreateDTO>> Handle(CreateEducationOrchestrator request, CancellationToken cancellationToken)
        {


            var resultIsVerifiedUser = await _mediator.Send(new IsVerifiedUserCommand(request.educationDTO.UserID));

            if (!resultIsVerifiedUser.IsSuccess)
            {
                return ResultDTO<EducationCreateDTO>.Faliure(resultIsVerifiedUser.Message);
            }

            var resultCreateEducationDTO = await _mediator.Send(new CreateEducationCommand(request.educationDTO));



            return resultCreateEducationDTO;
        }
    }
}
