using MediatR;
using Microsoft.EntityFrameworkCore;
using ResumeTemplate.DTO;
using ResumeTemplate.DTO.Skills;
using ResumeTemplate.Entities;
using ResumeTemplate.Helpers;

namespace ResumeTemplate.CQRS.Skills.Queries
{
    public record ViewAllSkillsQuery(SkillViewDTO skillViewDTO) : IRequest<ResultDTO<IEnumerable<SkillReturnViewDTO>>>;


    public class ViewAllSkillsQueryHandler : BaseRequestHandler<Skill, ViewAllSkillsQuery, ResultDTO<IEnumerable<SkillReturnViewDTO>>>
    {

        public ViewAllSkillsQueryHandler(RequestParameters<Skill> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<IEnumerable<SkillReturnViewDTO>>> Handle(ViewAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var skillsDTO = await _repository.GetAllPaginationAsync
                                        (
                                            request.skillViewDTO.pageNumber,
                                            request.skillViewDTO.pageSize
                                        )
                                        .Select(s => new SkillReturnViewDTO()
                                        {
                                            Name = s.Name
                                        }).ToListAsync();


            return ResultDTO<IEnumerable<SkillReturnViewDTO>>.Sucess(skillsDTO, "View Skills successfully!");
        }
    }
}
