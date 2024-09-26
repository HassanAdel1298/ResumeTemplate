using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeTemplate.Entities
{
    public class Education : BaseModel
    {
        public string Faculty { get; set; }
        public string University { get; set; }
        public float Grade { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }
    }
}
