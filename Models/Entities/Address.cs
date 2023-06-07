namespace Food_Mania.Models.Entities
{
    public class Address:BaseEntity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}