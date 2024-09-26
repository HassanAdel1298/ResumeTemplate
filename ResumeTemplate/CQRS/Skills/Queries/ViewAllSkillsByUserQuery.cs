using MediatR;
using ResumeTemplate.DTO.Skills;
using ResumeTemplate.DTO;
using ResumeTemplate.Entities;
using ResumeTemplate.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ResumeTemplate.CQRS.Skills.Queries
{
    public record ViewAllSkillsByUserQuery(SkillViewDTO skillViewDTO) : IRequest<ResultDTO<IEnumerable<SkillReturnViewDTO>>>;


    public class ViewAllSkillsByUserQueryHandler : BaseRequestHandler<UserSkills, ViewAllSkillsByUserQuery, ResultDTO<IEnumerable<SkillReturnViewDTO>>>
    {

        public ViewAllSkillsByUserQueryHandler(RequestParameters<UserSkills> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<IEnumerable<SkillReturnViewDTO>>> Handle(ViewAllSkillsByUserQuery request, CancellationToken cancellationToken)
        {
            var skillsDTO = await _repository.GetAllPaginationAsync
                                        (
                                            request.skillViewDTO.pageNumber,
                                            request.skillViewDTO.pageSize
                                        )
                                        .Where(s => s.UserID == request.skillViewDTO.userID)
                                        .Select(s => new SkillReturnViewDTO()
                                        {
                                            Name = s.Skill.Name
                                        }).ToListAsync();


            return ResultDTO<IEnumerable<SkillReturnViewDTO>>.Sucess(skillsDTO, "View Skills By User successfully!");
        }
    }
}
