using MediatR;
using Microsoft.EntityFrameworkCore;
using ResumeTemplate.DTO;
using ResumeTemplate.Entities;
using ResumeTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeTemplate.CQRS.Users.Commands
{
   
    public record IsVerifiedUserCommand(int UserID) : IRequest<ResultDTO<string>>;

    public class IsVerifiedUserCommandHandler : BaseRequestHandler<User, IsVerifiedUserCommand, ResultDTO<string>>
    {

        public IsVerifiedUserCommandHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<string>> Handle(IsVerifiedUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAllAsync()
                                .Where(u => u.ID == request.UserID 
                                        && u.IsEmailVerified)
                                .Select(u => u.Email)
                                .FirstOrDefaultAsync();


            if (user is null)
            {
                return ResultDTO<string>.Faliure("User ID isn't Verified");
            }

            return ResultDTO<string>.Sucess(user, "User ID is Verified");
        }

    }
}
