namespace ResumeTemplate.DTO.Educations
{
    public class EducationCreateDTO
    {
        public int ID { get; set; }
        public string Faculty { get; set; }
        public string University { get; set; }
        public float Grade { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserID { get; set; }
    }
}
