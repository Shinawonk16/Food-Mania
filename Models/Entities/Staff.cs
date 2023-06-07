using Food_Mania.Models.Enum;

namespace Food_Mania.Models.Entities
{
    public class Staff: BaseEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime DateOfBirth { get; set; }
        // public Gender Gender { get; set; }
        
    }
}