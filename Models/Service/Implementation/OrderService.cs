using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food_Mania.Models.Dtos;
using Food_Mania.Models.Entities;
using Food_Mania.Models.Repository.Interface;
using Food_Mania.Models.Service.Interface;
using Models.Dtos;
using Models.Repository.Interface;

namespace Food_Mania.Models.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IFoodOrderRepository _foodOrderRepository;

        private readonly IFoodRepository _foodRepository;
        private readonly ICustomerRepository _customerRepository;

        public OrderService(IOrderRepository orderRepository, IFoodOrderRepository foodOrderRepository, IFoodRepository foodRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _foodOrderRepository = foodOrderRepository;
            _foodRepository = foodRepository;
            _customerRepository = customerRepository;
        }

        public BaseResponse<OrderDto> CalculatePrice(string customerId, decimal price, int quantity)
        {
            var customer = _customerRepository.Get(e => e.UserId == customerId);
            if (customer.Wallet < price)
            {
                return new BaseResponse<OrderDto>
                {
                    Message = "Insuficient Balance Please Fund Your Wallet",
                    Status = false
                };
            }
            customer.Wallet -= (price * quantity);
            _customerRepository.Update(customer);
            return new BaseResponse<OrderDto>
            {
                Message = $"Your Balance is {customer.Wallet}",
                Status = true
            };
        }

        public BaseResponse<OrderDto> CreateOrder( string customerId, string foodId, CreateOrderRequestModel model)
        {
            var food = _foodRepository.Get(foodId);
            if (food == null)
            {
                return new BaseResponse<OrderDto>
                {
                    Message = "Something went wrong",
                    Status = false,
                };
            }
            var customer = _customerRepository.Get(customerId);
            var order = new Order
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                // CustomerId = customer.Id,
                IsDelivered = model.IsDelivered,
                Address = new Address
                {
                    City = model.City,
                    State = model.State,
                    Street = model.Street
                },
                CustomerId = customerId,
                Customer = customer,
                
            };

            var ord = _orderRepository.Create(order);
            var FoodOrder = new FoodOrder
            {
                FoodId = food.Id,
                OrderId = ord.Id,
                Order = ord,
                Food = food,
                Quantity = model.Quantity,
            
            };
            _foodOrderRepository.Create(FoodOrder);
            _orderRepository.Save();

            // _orderRepository.Insert(FoodOrder);
            return new BaseResponse<OrderDto>
            {
                Message = "Successfully Ordered",
                Status = true
            };
        }

        public BaseResponse<OrderDto> GetOrderById(string id)
        {
            var order = _orderRepository.Get(id);
            // var d = order.Address.Id;
            if (order == null)
            {
                return new BaseResponse<OrderDto>
                {
                    Message = "Order Not found",
                    Status = false,
                };
            }
            return new BaseResponse<OrderDto>
            {
                Data = new OrderDto
                {
                    IsDelivered = order.IsDelivered,
                    State = order.Address.State,
                    City = order.Address.City,
                    Street = order.Address.Street,
                    CustomerDto = new CustomerDto
                    {
                        Users = new UserDto()
                        {
                            UserName = $"{order.Customer.User.LastName} {order.Customer.User.FirstName}",
                            Email = order.Customer.User.Email,
                            PhoneNumber = order.Customer.User.PhoneNumber,
                            Id = order.Customer.Id,
                        },

                        Gender = order.Customer.User.Gender,
                        Wallet = order.Customer.Wallet,

                    }
                }
            };
        }

        public BaseResponse<OrderDto> UpdateStatus(string id)
        {
            var order = _orderRepository.Get(id);
            if (order == null)
            {
                return new BaseResponse<OrderDto>
                {
                    Message = "Order not found",
                    Status = false
                };
            }
            order.IsDelivered = true;
            order.IsDeleted = true;
            _orderRepository.Update(order);
            return new BaseResponse<OrderDto>
            {
                Message = "Order Delivered Successfully",
                Status = true,
            };
        }

        public BaseResponse<IEnumerable<FoodOrderDto>> ViewOrders()
        {
            var orders = _orderRepository.Orders();
            if (orders != null)
            {
                return new BaseResponse<IEnumerable<FoodOrderDto>>
                {

                    Data = orders.Select(x => new FoodOrderDto
                    {
                        IsDelivered = x.Order.IsDelivered,
                        State = x.Order.Address.State,
                        City = x.Order.Address.City,
                        Street = x.Order.Address.Street,
                        OrderDto = new OrderDto
                        {
                            Id = x.Order.Id,
                            CreatedAt = x.CreatedAt,
                        },
                        FoodDto = new FoodDto
                        {
                            Id = x.Food.Id,
                            Type = x.Food.Type,
                            Price = Math.Round(x.Food.Price, 4),
                            NumberOfPlates = x.Food.NumberOfPlates,
                            Image = x.Food.Image,
                            Description = x.Food.Description,
                            Status = x.Food.Status,
                            AvailableTime = x.Food.AvailableTime,
                        },
                        CustomerDto = new CustomerDto
                        {
                            Users = new UserDto()
                            {
                                UserName = $"{x.Order.Customer.User.LastName} {x.Order.Customer.User.FirstName}",
                                Email = x.Order.Customer.User.Email,
                                PhoneNumber = x.Order.Customer.User.PhoneNumber,
                                Id = x.Order.Customer.Id,
                            },

                            Gender = x.Order.Customer.User.Gender,
                            Wallet = x.Order.Customer.Wallet,
                        }
                    }).ToList(),
                    Message = "SuccessFul",
                    Status = true,
                };
            }
            else
            {
                return new BaseResponse<IEnumerable<FoodOrderDto>>
                {
                    Message = "Error",
                    Status = false,
                };
            }

        }
    }
}