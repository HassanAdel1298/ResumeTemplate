using AutoMapper;
using ResumeTemplate.DTO.Educations;
using ResumeTemplate.Entities;
using ResumeTemplate.ViewModel.Educations;

namespace ResumeTemplate.Profiles
{
    public class EducationProfile : Profile
    {
        public EducationProfile()
        {
            CreateMap<EducationCreateViewModel, EducationCreateDTO>().ReverseMap();
            CreateMap<EducationCreateDTO, Education>().ReverseMap();


            CreateMap<ViewEducationViewModel, EducationViewDTO>().ReverseMap();


            CreateMap<EducationUpdateViewModel, EducationUpdateDTO>().ReverseMap();
            CreateMap<EducationUpdateDTO, Education>().ReverseMap();

        }
    }
}
