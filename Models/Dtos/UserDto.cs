using System.ComponentModel.DataAnnotations;
using Food_Mania.Models.Enum;

namespace Food_Mania.Models.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }
        public Role Role { get; set; }

        public string Email { get; set; }
    }
    public class CreateUserRequestModel
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
        public Gender Gender { get; set; }

    }
    public class UpdateUserRequestModels
    {
        [Required]
        [MaxLength(50), MinLength(5)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50), MinLength(5)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50), MinLength(10)]
        public string Email{get;set;}

    }
     public class LoginUserRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}