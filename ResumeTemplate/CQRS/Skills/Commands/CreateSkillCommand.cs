using MediatR;
using Microsoft.EntityFrameworkCore;
using ResumeTemplate.DTO;
using ResumeTemplate.DTO.Skills;
using ResumeTemplate.DTO.Users;
using ResumeTemplate.Entities;
using ResumeTemplate.Helpers;

namespace ResumeTemplate.CQRS.Skills.Commands
{

    public record CreateSkillCommand(SkillCreateDTO skillDTO) : IRequest<ResultDTO<SkillCreateDTO>>;



    public class CreateSkillCommandHandler : BaseRequestHandler<Skill, CreateSkillCommand, ResultDTO<SkillCreateDTO>>
    {

        public CreateSkillCommandHandler(RequestParameters<Skill> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<SkillCreateDTO>> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync()
                               .Where(s => s.Name == request.skillDTO.Name)
                               .FirstOrDefaultAsync();

            if (result is not null)
            {
                var resultskillDTO = result.MapOne<SkillCreateDTO>();

                return ResultDTO<SkillCreateDTO>.Sucess(resultskillDTO, "Sill is already Created!");
            }


            var skill = request.skillDTO.MapOne<Skill>();

            await _repository.AddAsync(skill);

            await _repository.SaveChangesAsync();

            var skillDTO = skill.MapOne<SkillCreateDTO>();

            return ResultDTO<SkillCreateDTO>.Sucess(skillDTO, "Project created successfully!");
        }
    }
}
