using MediatR;
using Microsoft.EntityFrameworkCore;
using ResumeTemplate.DTO;
using ResumeTemplate.DTO.Users;
using ResumeTemplate.Entities;
using ResumeTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeTemplate.CQRS.Users.Commands
{
    public record LoginUserCommand(LoginUserDTO loginUserDTO) : IRequest<ResultDTO<string>>;

    public class LoginUserCommandHandler : BaseRequestHandler<User,LoginUserCommand, ResultDTO<string>>
    {

        public LoginUserCommandHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAllAsync()
                                    .Where(u => u.Email == request.loginUserDTO.Email
                                            && u.IsEmailVerified)
                                    .FirstOrDefaultAsync();

            if (user is null 
                || !BCrypt.Net.BCrypt.Verify(request.loginUserDTO.Password, user.PasswordHash))
            {
                return ResultDTO<string>.Faliure("Email or Password is incorrect");
            }

            var token = TokenGenerator.GenerateToken(user.ID.ToString(),user.Email,user.FullName);

            return ResultDTO<string>.Sucess(token, "User Login Successfully!");
        }

    }
}
