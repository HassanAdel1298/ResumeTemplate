using AutoMapper;
using ResumeTemplate.DTO.Experiences;
using ResumeTemplate.Entities;
using ResumeTemplate.ViewModel.Experiences;

namespace ResumeTemplate.Profiles
{
    public class ExperienceProfile : Profile
    {
        public ExperienceProfile()
        {
            CreateMap<EducationCreateViewModel, ExperienceCreateDTO>().ReverseMap();
            CreateMap<ExperienceCreateDTO, Experience>().ReverseMap();


            CreateMap<ViewEducationViewModel, ExperienceViewDTO>().ReverseMap();


            CreateMap<EducationUpdateViewModel, ExperienceUpdateDTO>().ReverseMap();
            CreateMap<ExperienceUpdateDTO, Experience>().ReverseMap();

        }
    }
}
