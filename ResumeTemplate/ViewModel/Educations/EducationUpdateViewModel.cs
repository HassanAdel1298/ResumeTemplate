using System.ComponentModel.DataAnnotations;

namespace ResumeTemplate.ViewModel.Educations
{
    public class EducationUpdateViewModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ID { get; set; }
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
