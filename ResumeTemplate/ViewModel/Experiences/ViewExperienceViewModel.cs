using System.ComponentModel.DataAnnotations;

namespace ResumeTemplate.ViewModel.Experiences
{
    public class ViewExperienceViewModel
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int pageNumber { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int pageSize { get; set; }
    }
}
