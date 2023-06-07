namespace Food_Mania.Models.Dtos
{
    public class OrderDto
    {
        
         public bool IsDelivered { get;set; }
        public string Street { get; set; }
        public string Id { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        // public int PostalCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public CustomerDto CustomerDto {get;set;}
    }
     public class CreateOrderRequestModel
    {
        // public string CustomerId { get; set; }   
        public int Quantity { get; set; }    
        public bool IsDelivered { get;set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PostalCode { get; set; }

        
    }
    public class UpdateOrderRequestModel
    {
        public bool IsDelivered {get;set;}
    }
}