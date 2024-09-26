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
    public record RegisterUserCommand(RegisterUserDTO registerUserDTO) : IRequest<ResultDTO<RegisterUserDTO>>;   


    public class RegisterUserCommandHandler : BaseRequestHandler<User,RegisterUserCommand, ResultDTO<RegisterUserDTO>>
    {

        public RegisterUserCommandHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO<RegisterUserDTO>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            if (request.registerUserDTO.Password != request.registerUserDTO.ConfirmPassword)
            {
                return ResultDTO<RegisterUserDTO>.Faliure("Confirm Password does not match the Password.");
            }

            var result = await _repository.GetAllAsync()
                               .Where(u => u.Email == request.registerUserDTO.Email)
                               .FirstOrDefaultAsync();

            if (result is not null)
            {
                return ResultDTO<RegisterUserDTO>.Faliure("Email is already registered!");
            }

            
           

            var user = request.registerUserDTO.MapOne<User>();

            user.PasswordHash = PasswordHashGenerator.CreatePasswordHash(request.registerUserDTO.Password);

            user.OTP = OTPGenerator.CreateOTP();
            user.Role = Role.User;

            user = await _repository.AddAsync(user);

            await _repository.SaveChangesAsync();

            var userDTO = user.MapOne<RegisterUserDTO>();

            return ResultDTO<RegisterUserDTO>.Sucess(userDTO, "User registred successfully!");
        }


        

        

    }
}
