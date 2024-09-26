using MediatR;
using ResumeTemplate.DTO.Skills;
using ResumeTemplate.DTO;
using ResumeTemplate.Entities;
using ResumeTemplate.Helpers;
using ResumeTemplate.DTO.Experiences;
using Microsoft.EntityFrameworkCore;

namespace ResumeTemplate.CQRS.Experiences.Queries
{
    public record ViewAllExperiencesByUserQuery(ExperienceViewDTO experienceViewDTO) : IRequest<ResultDTO<IEnumerable<ExperienceReturnViewDTO>>>;


    public class ViewAllExperiencesByUserQueryHandler : BaseRequestHandler<Experience, ViewAllExperiencesByUserQuery, ResultDTO<IEnumerable<ExperienceReturnViewDTO>>>
    {

        public ViewAllExperiencesByUserQueryHandler(RequestParameters<Experience> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<IEnumerable<ExperienceReturnViewDTO>>> Handle(ViewAllExperiencesByUserQuery request, CancellationToken cancellationToken)
        {
            var experiencesDTO = await _repository.GetAllPaginationAsync
                                        (
                                            request.experienceViewDTO.pageNumber,
                                            request.experienceViewDTO.pageSize
                                        )
                                        .Where(e => e.UserID == request.experienceViewDTO.userID)
                                        .Select(e => new ExperienceReturnViewDTO()
                                        {
                                            Title = e.Title,
                                            Description = e.Description,
                                            CompanyName = e.CompanyName,
                                            StartDate = e.StartDate,
                                            EndDate = e.EndDate
                                        }).ToListAsync();


            return ResultDTO<IEnumerable<ExperienceReturnViewDTO>>.Sucess(experiencesDTO, "View Experiences By User successfully!");
        }
    }
}
