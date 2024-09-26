using MediatR;
using ResumeTemplate.CQRS.Skills.Commands;
using ResumeTemplate.CQRS.Users.Commands;
using ResumeTemplate.DTO.Skills;
using ResumeTemplate.DTO;
using ResumeTemplate.Entities;
using ResumeTemplate.Helpers;
using ResumeTemplate.DTO.Experiences;
using ResumeTemplate.CQRS.Experiences.Commands;

namespace ResumeTemplate.CQRS.Experiences.Orchestrators
{
    public record CreateExperienceOrchestrator(ExperienceCreateDTO experienceDTO) : IRequest<ResultDTO<ExperienceCreateDTO>>;

    public class CreateExperienceOrchestratorHandler : BaseRequestHandler<Experience, CreateExperienceOrchestrator, ResultDTO<ExperienceCreateDTO>>
    {
        public CreateExperienceOrchestratorHandler(RequestParameters<Experience> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<ExperienceCreateDTO>> Handle(CreateExperienceOrchestrator request, CancellationToken cancellationToken)
        {


            var resultIsVerifiedUser = await _mediator.Send(new IsVerifiedUserCommand(request.experienceDTO.UserID));

            if (!resultIsVerifiedUser.IsSuccess)
            {
                return ResultDTO<ExperienceCreateDTO>.Faliure(resultIsVerifiedUser.Message);
            }

            var resultCreateExperienceDTO = await _mediator.Send(new CreateExperienceCommand(request.experienceDTO));



            return resultCreateExperienceDTO;
        }
    }
}
