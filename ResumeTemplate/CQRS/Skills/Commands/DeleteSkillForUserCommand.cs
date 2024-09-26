using MediatR;
using Microsoft.EntityFrameworkCore;
using ResumeTemplate.DTO;
using ResumeTemplate.DTO.Skills;
using ResumeTemplate.Entities;
using ResumeTemplate.Helpers;

namespace ResumeTemplate.CQRS.Skills.Commands
{

    public record DeleteSkillForUserCommand(SkillDeleteDTO skillDeleteDTO) : IRequest<ResultDTO<bool>>;

    public class DeleteSkillForUserCommandHandler : BaseRequestHandler<UserSkills, DeleteSkillForUserCommand, ResultDTO<bool>>
    {

        public DeleteSkillForUserCommandHandler(RequestParameters<UserSkills> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<bool>> Handle(DeleteSkillForUserCommand request, CancellationToken cancellationToken)
        {
            var userSkill = await _repository.GetAllAsync()
                                        .Where(
                                            us => us.SkillID == request.skillDeleteDTO.SkillID 
                                            && us.UserID == request.skillDeleteDTO.UserID
                                        )
                                        .FirstOrDefaultAsync();

            if (userSkill is null)
            {
                return ResultDTO<bool>.Faliure("Skill ID Not Found!");
            }

            _repository.DeleteAsync(userSkill);

            await _repository.SaveChangesAsync();

            return ResultDTO<bool>.Sucess(true, "Delete Project successfully!");
        }
    }
}
