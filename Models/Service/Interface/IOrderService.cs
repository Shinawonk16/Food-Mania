using Food_Mania.Models.Dtos;
using Food_Mania.Models.Entities;
using Models.Dtos;

namespace Food_Mania.Models.Service.Interface
{
    public interface IOrderService
    {
         BaseResponse<OrderDto> CalculatePrice(string id, decimal price,int quantity );
        BaseResponse<OrderDto> CreateOrder(string customerId, string id, CreateOrderRequestModel model);
        BaseResponse<OrderDto> GetOrderById(string id);
        BaseResponse<IEnumerable<FoodOrderDto>> ViewOrders();
        BaseResponse<OrderDto> UpdateStatus(string id);
    }
}