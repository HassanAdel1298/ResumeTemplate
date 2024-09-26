using MediatR;
using ResumeTemplate.CQRS.Skills.Commands;
using ResumeTemplate.CQRS.Users.Commands;
using ResumeTemplate.DTO;
using ResumeTemplate.DTO.Skills;
using ResumeTemplate.Entities;
using ResumeTemplate.Helpers;

namespace ResumeTemplate.CQRS.Skills.Orchestrators
{
    public record CreateSkillOrchestrator(SkillCreateDTO skillDTO) : IRequest<ResultDTO<SkillCreateDTO>>;

    public class CreateSkillOrchestratorHandler : BaseRequestHandler<Skill, CreateSkillOrchestrator, ResultDTO<SkillCreateDTO>>
    {
        public CreateSkillOrchestratorHandler(RequestParameters<Skill> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<SkillCreateDTO>> Handle(CreateSkillOrchestrator request, CancellationToken cancellationToken)
        {

            var resultCreateSkillDTO = await _mediator.Send(new CreateSkillCommand(request.skillDTO));

            var resultIsVerifiedUser = await _mediator.Send(new IsVerifiedUserCommand(request.skillDTO.UserID));

            if (!resultIsVerifiedUser.IsSuccess)
            {
                return ResultDTO<SkillCreateDTO>.Faliure(resultIsVerifiedUser.Message);
            }

            resultCreateSkillDTO.Data.UserID = request.skillDTO.UserID;

            var resultAddSkillToUser = await _mediator.Send(new AddSkillToUserCommand(resultCreateSkillDTO.Data));

            if (!resultAddSkillToUser.IsSuccess)
            {
                return ResultDTO<SkillCreateDTO>.Faliure(resultAddSkillToUser.Message);
            }

            return resultCreateSkillDTO;
        }
    }
}
