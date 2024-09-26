using MediatR;
using ResumeTemplate.DTO;
using ResumeTemplate.Entities;
using ResumeTemplate.Helpers;
using ResumeTemplate.DTO.Educations;
using Microsoft.EntityFrameworkCore;

namespace ResumeTemplate.CQRS.Educations.Commands
{
    public record UpdateEducationCommand(EducationUpdateDTO educationDTO) : IRequest<ResultDTO<EducationUpdateDTO>>;



    public class UpdateEducationCommandHandler : BaseRequestHandler<Education, UpdateEducationCommand, ResultDTO<EducationUpdateDTO>>
    {

        public UpdateEducationCommandHandler(RequestParameters<Education> requestParameters) : base(requestParameters)
        {
        }
        public override async Task<ResultDTO<EducationUpdateDTO>> Handle(UpdateEducationCommand request, CancellationToken cancellationToken)
        {
            var resultEducation = await _repository.GetAllAsync()
                                        .Where(t => t.ID == request.educationDTO.ID
                                                && t.UserID == request.educationDTO.UserID
                                        )
                                        .FirstOrDefaultAsync();

            if (resultEducation is null)
            {
                return ResultDTO<EducationUpdateDTO>.Faliure("Education ID Not Found or isn't have this Education!");
            }

            var education = request.educationDTO.MapOne<Education>();

            await _repository.UpdateAsync(education);

            await _repository.SaveChangesAsync();

            var educationDTO = education.MapOne<EducationUpdateDTO>();

            return ResultDTO<EducationUpdateDTO>.Sucess(educationDTO, "Education Updated successfully!");
        }
    }
}
