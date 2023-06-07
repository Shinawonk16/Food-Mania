using System.ComponentModel.DataAnnotations;
using Food_Mania.Models.Enum;

namespace Food_Mania.Models.Dtos
{
    public class StaffDto
    {
        public UserDto User { get; set; }
        public DateTime DathOfBirth { get; set; }
        public Gender Gender { get; set; }
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class CreateStaffRequestModel
    {
        [MaxLength(50), MinLength(5)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50), MinLength(5)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50), MinLength(10)]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public Role Role { get; set; }
        public DateTime DathOfBirth { get; set; }
        public DateTime CreatedAt { get; set; } 
        
        public Gender Gender { get; set; }
    }
    public class UpdateStaffRequestModel
    {
        [Required]
        [MaxLength(50), MinLength(5)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50), MinLength(5)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50), MinLength(10)]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        
        public DateTime UpdatedAt { get; set; } 
        public DateTime DateOfBirth { get; set; } 
        public Role Role { get; set; }
    }
}