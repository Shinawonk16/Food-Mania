using Food_Mania.Models.Dtos;
using Food_Mania.Models.Entities;
using Food_Mania.Models.Repository.Interface;
using Food_Mania.Models.Service.Interface;

namespace Food_Mania.Models.Service.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;

        public CustomerService(IUserRepository userRepository, ICustomerRepository customerRepository)
        {
            _userRepository = userRepository;
            _customerRepository = customerRepository;
        }

        public BaseResponse<CustomerDto> CheckWallet(string id)
        {
            var customer = _customerRepository.Get(a => a.User.Id == id);
            if (customer == null)
            {
                return new BaseResponse<CustomerDto>
                {
                    Message = "Wallet not found",
                    Status = false

                };
            }
            return new BaseResponse<CustomerDto>
            {
                Message = $"Your balance is N{customer.Wallet}",
                Status = true,
                Data = new CustomerDto
                {
                    UserName = customer.User.FirstName + customer.User.LastName,
                    Wallet = customer.Wallet
                }
            };
        }

        public BaseResponse<CustomerDto> Create(CreateCustomerRequestModel model)
        {
            var check = _customerRepository.CheckIfExist(model.Email);
            if (check == true)
            {
                return new BaseResponse<CustomerDto>
                {
                    Message = "Email Already Exist",
                    Status = false
                };
            }
            // var hash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var customer = new Customer
            {
                Wallet = 0.00m,

                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                User = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Gender = model.Gender,
                    UserName = (model.FirstName + model.LastName),
                    Password = model.Password,
                    CreatedAt = DateTime.Now,
                    Role = model.Role,
                    UpdatedAt = DateTime.Now,
                }
            };
            // var user = new User
            // {
            //     FirstName = model.FirstName,
            //     LastName = model.LastName,
            //     Email = model.Email,
            //     PhoneNumber = model.PhoneNumber,
            //     Gender = model.Gender,
            //     Password = model.Password,
            //     CreatedAt = DateTime.Now,
            //     UpdatedAt = DateTime.Now,
            // };
            _customerRepository.Create(customer);
            // _userRepository.Create(user);
            _customerRepository.Save();
            return new BaseResponse<CustomerDto>
            {
                Message = "Successfully Registered",
                Status = true,
                Data = new CustomerDto
                {
                    Wallet = 0.00m,
                    Gender = model.Gender,
                    Users = new UserDto
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                    }
                }
            };
        }

        public BaseResponse<CustomerDto> FundWallet(string id, decimal amount)
        {
            var customer = _customerRepository.Get(a => a.User.Id == id);
            customer.Wallet += amount;
            _customerRepository.Update(customer);
            return new BaseResponse<CustomerDto>
            {
                Message = "Wallet Successfully Funded",
                Status = true
            };
        }

        public BaseResponse<CustomerDto> Get(string email, string password)
        {
            var customer = _customerRepository.Get(e => e.User.Email == email);
            if (customer != null && customer.User.Password == password)
            {
                return new BaseResponse<CustomerDto>()
                {
                    Message = "Valid  Credentials",
                    Status = true,
                    Data = new CustomerDto
                    {
                        Wallet = customer.Wallet,
                        Users = new UserDto
                        {
                            Gender = customer.User.Gender,
                            Id = customer.Id,
                            UserName = $"{customer.User.LastName} {customer.User.FirstName}",
                            Email = customer.User.Email,
                            PhoneNumber = customer.User.PhoneNumber,
                        }
                    }
                };
            }
            return new BaseResponse<CustomerDto>
            {
                Message = "Invalid  Credentials",
                Status = false
            };

        }

        public BaseResponse<IEnumerable<CustomerDto>> GetAll()
        {
            var all = _customerRepository.GetAllCustomers();
            return new BaseResponse<IEnumerable<CustomerDto>>
            {
                Data = all.Select(x => new CustomerDto
                {
                    CreatedAt = x.CreatedAt,
                    Users = new UserDto
                    {
                        Gender = x.User.Gender,
                        Role = x.User.Role,
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        Id = x.Id,
                        UserName = $"{x.User.LastName} {x.User.FirstName}",
                        Email = x.User.Email,
                        PhoneNumber = x.User.PhoneNumber,
                    }
                }).ToList()
            };
        }

        public BaseResponse<CustomerDto> GetById(string id)
        {
            var customer = _customerRepository.Get(e => e.User.Id == id);
            if (customer != null)
            {
                return new BaseResponse<CustomerDto>
                {
                    Message = "Found",
                    Status = true,
                    Data = new CustomerDto
                    {
                        Wallet = customer.Wallet,
                        Users = new UserDto
                        {
                            
                            FirstName = customer.User.FirstName,
                            LastName = customer.User.LastName,
                            Gender = customer.User.Gender,
                            Role = customer.User.Role,
                            Id = customer.Id,
                            UserName = $"{customer.User.LastName} {customer.User.FirstName}",
                            Email = customer.User.Email,
                            PhoneNumber = customer.User.PhoneNumber,
                        }
                    }
                };
            }
            return new BaseResponse<CustomerDto>
            {
                Message = "Not Found",
                Status = false
            };
        }

        public BaseResponse<IEnumerable<CustomerDto>> GetSelected(string city)
        {
            var customer = _customerRepository.GetSelected(a => a.User.Address.City == city);
            if (customer != null)
            {
                return new BaseResponse<IEnumerable<CustomerDto>>
                {
                    Message = "Found",
                    Status = true,
                    Data = customer.Select(x => new CustomerDto
                    {

                        Users = new UserDto
                        {
                            Gender = x.User.Gender,
                            Id = x.Id,
                            UserName = $"{x.User.LastName} {x.User.FirstName}",
                            Email = x.User.Email,
                            PhoneNumber = x.User.PhoneNumber,
                        }
                    })
                };
            }
            return new BaseResponse<IEnumerable<CustomerDto>>
            {
                Message = "Not Found",
                Status = false
            };

        }

        public BaseResponse<CustomerDto> Update(string id, UpdateCustomerRequestModel model)
        {
            var customer = _customerRepository.Get(x => x.Id == id);
            if (customer == null)
            {
                return new BaseResponse<CustomerDto>
                {
                    Message = "Account Not Found",
                    Status = false
                };
            }
            customer.User.FirstName = model.FirstName;
            customer.User.LastName = model.LastName;
            customer.User.PhoneNumber = model.PhoneNumber;
            customer.User.Email = model.Email;
            customer.UpdatedAt = DateTime.Now;
            var response = _customerRepository.Update(customer);
            return new BaseResponse<CustomerDto>
            {
                Message = "Account Updated Successfully",
                Status = true
            };

        }

        public BaseResponse<CustomerDto> UpdateWallet(string id, UpdateWalletRequestModel model)
        {
            var customer = _customerRepository.Get(id);
            customer.Wallet += model.Wallet;
            _customerRepository.Update(customer);

            return new BaseResponse<CustomerDto>
            {
                Message = "Wallet Updated Successfully",
                Status = true,
            };
        }

        public BaseResponse<CustomerDto> VeiwProfile(string email)
        {
            var profile = _customerRepository.Get(a => a.User.Email == email);
            if (profile == null)
            {
                return new BaseResponse<CustomerDto>
                {
                    Message = "Profile not found",
                    Status = false
                };
            }

            return new BaseResponse<CustomerDto>
            {
                Message = "Profile was found successfully ",
                Status = true,
                Data = new CustomerDto
                {

                    Wallet = profile.Wallet,
                    Users = new UserDto()
                    {
                        Gender = profile.User.Gender,
                        UserName = profile.User.FirstName + " " + profile.User.LastName,
                        Email = profile.User.Email,
                        PhoneNumber = profile.User.PhoneNumber
                    }
                }

            };
        }
    }
}