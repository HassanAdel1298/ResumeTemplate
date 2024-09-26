
using System.ComponentModel.DataAnnotations;

namespace ResumeTemplate.Entities
{
    public class BaseModel
    {
        [Key]
        public int ID { get; set; }

        public bool IsDeleted { get; set; }

    }
}
