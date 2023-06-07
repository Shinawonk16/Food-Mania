using System.ComponentModel.DataAnnotations;
using Food_Mania.Models.Enum;

namespace Food_Mania.Models.Dtos
{
    public class CustomerDto
    {
        //  public string Id {get;set;}
        public UserDto Users { get; set; }
        public decimal Wallet { get; set; }
        public Gender Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UserName { get; set; }

    }

    public class CreateCustomerRequestModel
    {
        [MaxLength(50), MinLength(5)]
        public string FirstName { get; set; }
        public Gender Gender { get; set; }
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
        public Role Role { get; set; } = Role.Customer;


    }
    public class UpdateCustomerRequestModel
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

    }
    public class UpdateWalletRequestModel
    {
        public decimal Wallet { get; set; }
       
    }

}