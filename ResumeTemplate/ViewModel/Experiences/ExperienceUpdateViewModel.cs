﻿using System.ComponentModel.DataAnnotations;

namespace ResumeTemplate.ViewModel.Experiences
{
    public class ExperienceUpdateViewModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
