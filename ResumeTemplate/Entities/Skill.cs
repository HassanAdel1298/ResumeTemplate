
namespace ResumeTemplate.Entities
{
    public class Skill : BaseModel
    {
        public string Name { get; set; }

        public ICollection<UserSkills> UserSkills { get; set; }
    }
}
