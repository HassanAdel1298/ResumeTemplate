using MediatR;
using ResumeTemplate.DTO;
using ResumeTemplate.Entities;
using ResumeTemplate.Helpers;
using ResumeTemplate.DTO.Educations;

namespace ResumeTemplate.CQRS.Educations.Commands
{

    public record CreateEducationCommand(EducationCreateDTO educationDTO) : IRequest<ResultDTO<EducationCreateDTO>>;



    public class CreateEducationCommandHandler : BaseRequestHandler<Education, CreateEducationCommand, ResultDTO<EducationCreateDTO>>
    {

        public CreateEducationCommandHandler(RequestParameters<Education> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<EducationCreateDTO>> Handle(CreateEducationCommand request, CancellationToken cancellationToken)
        {


            var education = request.educationDTO.MapOne<Education>();

            await _repository.AddAsync(education);

            await _repository.SaveChangesAsync();

            var educationDTO = education.MapOne<EducationCreateDTO>();

            return ResultDTO<EducationCreateDTO>.Sucess(educationDTO, "Education created successfully!");
        }
    }
}
