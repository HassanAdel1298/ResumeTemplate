using System.ComponentModel.DataAnnotations;

namespace ResumeTemplate.ViewModel.Skills
{
    public class ViewSkillViewModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int pageNumber { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int pageSize { get; set; }

    }
}
