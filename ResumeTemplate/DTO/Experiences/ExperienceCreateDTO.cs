namespace ResumeTemplate.DTO.Experiences
{
    public class ExperienceCreateDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserID { get; set; }
    }
}
