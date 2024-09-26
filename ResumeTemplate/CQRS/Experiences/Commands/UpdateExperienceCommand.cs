using MediatR;
using Microsoft.EntityFrameworkCore;
using ResumeTemplate.DTO;
using ResumeTemplate.DTO.Experiences;
using ResumeTemplate.Entities;
using ResumeTemplate.Helpers;

namespace ResumeTemplate.CQRS.Experiences.Commands
{
    public record UpdateExperienceCommand(ExperienceUpdateDTO experienceDTO) : IRequest<ResultDTO<ExperienceUpdateDTO>>;



    public class UpdateExperienceCommandHandler : BaseRequestHandler<Experience, UpdateExperienceCommand, ResultDTO<ExperienceUpdateDTO>>
    {

        public UpdateExperienceCommandHandler(RequestParameters<Experience> requestParameters) : base(requestParameters)
        {
        }
        public override async Task<ResultDTO<ExperienceUpdateDTO>> Handle(UpdateExperienceCommand request, CancellationToken cancellationToken)
        {
            var resultExperience = await _repository.GetAllAsync()
                                        .Where(t => t.ID == request.experienceDTO.ID
                                                && t.UserID == request.experienceDTO.UserID
                                        )
                                        .FirstOrDefaultAsync();

            if (resultExperience is null)
            {
                return ResultDTO<ExperienceUpdateDTO>.Faliure("Experience ID Not Found or isn't have this Experience!");
            }

            var experience = request.experienceDTO.MapOne<Experience>();

            await _repository.UpdateAsync(experience);

            await _repository.SaveChangesAsync();

            var experienceDTO = experience.MapOne<ExperienceUpdateDTO>();

            return ResultDTO<ExperienceUpdateDTO>.Sucess(experienceDTO, "Experience Updated successfully!");
        }
    }
}
