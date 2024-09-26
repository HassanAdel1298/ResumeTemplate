using MediatR;
using Microsoft.EntityFrameworkCore;
using ResumeTemplate.DTO;
using ResumeTemplate.DTO.Skills;
using ResumeTemplate.Entities;
using ResumeTemplate.Helpers;

namespace ResumeTemplate.CQRS.Skills.Commands
{
    public record AddSkillToUserCommand(SkillCreateDTO skillDTO) : IRequest<ResultDTO<bool>>;


    public class AddSkillToUserCommandHandler : BaseRequestHandler<UserSkills, AddSkillToUserCommand, ResultDTO<bool>>
    {

        public AddSkillToUserCommandHandler(RequestParameters<UserSkills> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<bool>> Handle(AddSkillToUserCommand request, CancellationToken cancellationToken)
        {
     
            var useProject = await _repository.GetAllAsync()
                               .Where(up => up.SkillID == request.skillDTO.ID
                                        && up.UserID == request.skillDTO.UserID)
                               .FirstOrDefaultAsync();

            if (useProject is not null)
            {
                return ResultDTO<bool>.Faliure("User ID is already have this Skill!");
            }

            UserSkills newUserSkills = new UserSkills()
            {
                UserID = request.skillDTO.UserID,
                SkillID = request.skillDTO.ID,
            };

            await _repository.AddAsync(newUserSkills);

            await _repository.SaveChangesAsync();

            return ResultDTO<bool>.Sucess(true, "Add Skill to User successfully!");
        }
    }
}
