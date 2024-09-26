using System.ComponentModel.DataAnnotations;

namespace ResumeTemplate.ViewModel.Skills
{
    public class SkillCreateViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
