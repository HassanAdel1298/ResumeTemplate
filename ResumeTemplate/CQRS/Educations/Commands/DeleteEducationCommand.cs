using MediatR;
using ResumeTemplate.DTO;
using ResumeTemplate.Entities;
using ResumeTemplate.Helpers;
using ResumeTemplate.DTO.Educations;
using Microsoft.EntityFrameworkCore;

namespace ResumeTemplate.CQRS.Educations.Commands
{
    public record DeleteEducationCommand(EducationDeleteDTO educationDeleteDTO) : IRequest<ResultDTO<bool>>;

    public class DeleteEducationCommandHandler : BaseRequestHandler<Education, DeleteEducationCommand, ResultDTO<bool>>
    {

        public DeleteEducationCommandHandler(RequestParameters<Education> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<bool>> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
        {
            var education = await _repository.GetAllAsync()
                                        .Where(
                                            us => us.ID == request.educationDeleteDTO.EducationID
                                            && us.UserID == request.educationDeleteDTO.UserID
                                        )
                                        .FirstOrDefaultAsync();

            if (education is null)
            {
                return ResultDTO<bool>.Faliure("Education ID Not Found!");
            }

            _repository.DeleteAsync(education);

            await _repository.SaveChangesAsync();

            return ResultDTO<bool>.Sucess(true, "Delete Education successfully!");
        }
    }
}
