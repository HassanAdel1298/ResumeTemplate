using System.ComponentModel.DataAnnotations;

namespace ResumeTemplate.ViewModel.Educations
{
    public class EducationCreateViewModel
    {
        [Required]
        public string Faculty { get; set; }
        [Required]
        public string University { get; set; }
        [Required]
        [Range(0, 4)]
        public float Grade { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
