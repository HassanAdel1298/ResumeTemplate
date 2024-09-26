

using AutoMapper;
using ResumeTemplate.DTO.Users;
using ResumeTemplate.Entities;
using ResumeTemplate.ViewModel.Users;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ResumeTemplate.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<RegisterViewModel, RegisterUserDTO>().ReverseMap();
            CreateMap<RegisterUserDTO, User>().ReverseMap();


            CreateMap<LoginViewModel, LoginUserDTO>().ReverseMap();
            CreateMap<LoginUserDTO, User>().ReverseMap();

            CreateMap<VerifyAccountViewModel, VerifyUserDTO>().ReverseMap();

        }
    }
}
