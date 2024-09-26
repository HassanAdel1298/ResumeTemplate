using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeTemplate.Entities
{
    public class Experience : BaseModel
    {
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
