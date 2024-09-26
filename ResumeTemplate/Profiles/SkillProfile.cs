using AutoMapper;
using ResumeTemplate.DTO.Skills;
using ResumeTemplate.Entities;
using ResumeTemplate.ViewModel.Skills;

namespace ResumeTemplate.Profiles
{
    public class SkillProfile : Profile
    {
        public SkillProfile()
        {
            CreateMap<SkillCreateViewModel, SkillCreateDTO>().ReverseMap();
            CreateMap<SkillCreateDTO, Skill>().ReverseMap();

            CreateMap<ViewSkillViewModel, SkillViewDTO>().ReverseMap();

        }
    }
}
