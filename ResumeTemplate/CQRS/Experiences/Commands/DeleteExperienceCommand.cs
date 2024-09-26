using MediatR;
using ResumeTemplate.DTO.Skills;
using ResumeTemplate.DTO;
using ResumeTemplate.Entities;
using ResumeTemplate.Helpers;
using ResumeTemplate.DTO.Experiences;
using Microsoft.EntityFrameworkCore;

namespace ResumeTemplate.CQRS.Experiences.Commands
{

    public record DeleteExperienceCommand(ExperienceDeleteDTO experienceDeleteDTO) : IRequest<ResultDTO<bool>>;

    public class DeleteExperienceCommandHandler : BaseRequestHandler<Experience, DeleteExperienceCommand, ResultDTO<bool>>
    {

        public DeleteExperienceCommandHandler(RequestParameters<Experience> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<bool>> Handle(DeleteExperienceCommand request, CancellationToken cancellationToken)
        {
            var experience = await _repository.GetAllAsync()
                                        .Where(
                                            us => us.ID == request.experienceDeleteDTO.ExperienceID
                                            && us.UserID == request.experienceDeleteDTO.UserID
                                        )
                                        .FirstOrDefaultAsync();

            if (experience is null)
            {
                return ResultDTO<bool>.Faliure("Experience ID Not Found!");
            }

            _repository.DeleteAsync(experience);

            await _repository.SaveChangesAsync();

            return ResultDTO<bool>.Sucess(true, "Delete Experience successfully!");
        }
    }
}
