using MediatR;
using ResumeTemplate.DTO.Experiences;
using ResumeTemplate.DTO;
using ResumeTemplate.Entities;
using ResumeTemplate.Helpers;
using ResumeTemplate.DTO.Educations;
using Microsoft.EntityFrameworkCore;

namespace ResumeTemplate.CQRS.Educations.Queries
{
    public record ViewAllEducationsByUserQuery(EducationViewDTO educationViewDTO) : IRequest<ResultDTO<IEnumerable<EducationReturnViewDTO>>>;


    public class ViewAllEducationsByUserQueryHandler : BaseRequestHandler<Education, ViewAllEducationsByUserQuery, ResultDTO<IEnumerable<EducationReturnViewDTO>>>
    {

        public ViewAllEducationsByUserQueryHandler(RequestParameters<Education> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<IEnumerable<EducationReturnViewDTO>>> Handle(ViewAllEducationsByUserQuery request, CancellationToken cancellationToken)
        {
            var educationsDTO = await _repository.GetAllPaginationAsync
                                        (
                                            request.educationViewDTO.pageNumber,
                                            request.educationViewDTO.pageSize
                                        )
                                        .Where(e => e.UserID == request.educationViewDTO.userID)
                                        .Select(e => new EducationReturnViewDTO()
                                        {
                                            Faculty = e.Faculty,
                                            University = e.University,
                                            Grade = e.Grade,
                                            StartDate = e.StartDate,
                                            EndDate = e.EndDate
                                        }).ToListAsync();


            return ResultDTO<IEnumerable<EducationReturnViewDTO>>.Sucess(educationsDTO, "View Educations By User successfully!");
        }
    }
}
