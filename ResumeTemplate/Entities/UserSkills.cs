using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeTemplate.Entities
{
    public class UserSkills : BaseModel
    {
        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }


        [ForeignKey("Skill")]
        public int SkillID { get; set; }
        public Skill Skill { get; set; }
    }
}
