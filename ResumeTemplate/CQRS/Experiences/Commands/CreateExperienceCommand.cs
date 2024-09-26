using MediatR;
using ResumeTemplate.DTO;
using ResumeTemplate.DTO.Experiences;
using ResumeTemplate.Entities;
using ResumeTemplate.Helpers;

namespace ResumeTemplate.CQRS.Experiences.Commands
{

    public record CreateExperienceCommand(ExperienceCreateDTO experienceDTO) : IRequest<ResultDTO<ExperienceCreateDTO>>;



    public class CreateExperienceCommandHandler : BaseRequestHandler<Experience, CreateExperienceCommand, ResultDTO<ExperienceCreateDTO>>
    {

        public CreateExperienceCommandHandler(RequestParameters<Experience> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<ExperienceCreateDTO>> Handle(CreateExperienceCommand request, CancellationToken cancellationToken)
        {


            var experience = request.experienceDTO.MapOne<Experience>();

            await _repository.AddAsync(experience);

            await _repository.SaveChangesAsync();

            var experienceDTO = experience.MapOne<ExperienceCreateDTO>();

            return ResultDTO<ExperienceCreateDTO>.Sucess(experienceDTO, "Experience created successfully!");
        }
    }
}
