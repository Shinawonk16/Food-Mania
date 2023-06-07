using Food_Mania.Models.Enum;

namespace Food_Mania.Models.Entities
{
    public class User: BaseEntity
    {
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string AddressId { get; set; }
        public Address Address { get; set; }
        public Role Role { get; set; }
        public Gender Gender { get; set; }
        public Staff Staff { get; set; }
        public Customer Customer { get; set; }




        

    }
}