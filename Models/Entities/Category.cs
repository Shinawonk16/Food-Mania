using Food_Mania.Models.Entities;

namespace Models.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public List<Food> Foods{get;set;}
    }
}