using Food_Mania.Models.Dtos;
using Food_Mania.Models.Entities;
using Food_Mania.Models.Enum;
using Food_Mania.Models.Repository.Interface;
using Models.Service.Interface;

namespace Models.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public BaseResponse<UserDto> Create(CreateUserRequestModel model)
        {
            var check = _userRepository.CheckIfExist(model.Email);
            if (check == true)
            {
                return new BaseResponse<UserDto>
                {
                    Message = "Email Already Exist",
                    Status = false
                };
            }
            //  var hash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var user = new User
            {

                Gender = model.Gender,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Password = model.Password

            };
            _userRepository.Create(user);
            _userRepository.Save();
            return new BaseResponse<UserDto>
            {
                Message = "Successfully Registered",
                Status = true,
                Data = new UserDto
                {

                    Gender = model.Gender,
                    // FirstName = model.FirstName,
                    // LastName = model.LastName,
                    UserName = model.FirstName + " " + model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,

                }

            };
        }

        public BaseResponse<UserDto> Get(string email, string password)
        {
            var user = _userRepository.Get(e => e.Email == email);
            if (user != null && user.Password == password)
            {
                return new BaseResponse<UserDto>()
                {
                    Message = "Valid  Credentials",
                    Status = true,
                    Data = new UserDto
                    {

                        Gender = user.Gender,
                        Id = user.Id,
                        UserName = $"{user.LastName} {user.FirstName}",
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,

                    }
                };
            }
            return new BaseResponse<UserDto>
            {
                Message = "Invalid  Credentials",
                Status = false
            };
        }

        public BaseResponse<IEnumerable<UserDto>> GetAll()
        {
            var user = _userRepository.GetUsers();
            return new BaseResponse<IEnumerable<UserDto>>
            {
                Data = user.Select(x => new UserDto
                {
                    Gender = x.Gender,
                    Id = x.Id,
                    UserName = $"{x.LastName} {x.FirstName}",
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,

                }
                )
            };
        }

        public BaseResponse<UserDto> GetById(string id)
        {
            var get = _userRepository.Get(id);
            if (get is not null)
            {
                return new BaseResponse<UserDto>()
                {
                    Message = "Credentials found",
                    Status = true,
                    Data = new UserDto
                    {

                        Gender = get.Gender,
                        Id = get.Id,
                        UserName = $"{get.LastName} {get.FirstName}",
                        Email = get.Email,
                        PhoneNumber = get.PhoneNumber,

                    }
                };
            }
            return new BaseResponse<UserDto>()
            {
                Message = "inValid  Credentials",
                Status = false,
            };
        }

        public BaseResponse<UserDto> GetByRole(Role role)
        {
            var checkByRole = _userRepository.GetUserByRole(role);
            if (checkByRole is not null)
            {
                return new BaseResponse<UserDto>()
                {
                    Message = $"user with {role.ToString()} found",
                    Status = true,
                    Data = new UserDto
                    {

                        Gender = checkByRole.Gender,
                        Id = checkByRole.Id,
                        UserName = $"{checkByRole.LastName} {checkByRole.FirstName}",
                        Email = checkByRole.Email,
                        PhoneNumber = checkByRole.PhoneNumber,

                    }
                };
            }
            return new BaseResponse<UserDto>()
            {
                Message = "InValid  Credentials",
                Status = false,
            };
        }

        public BaseResponse<UserDto> Login(LoginUserRequestModel model)
        {
            var user = _userRepository.Login(model.Email,model.Password);
             if (user != null)
            {
                var userLogin =  new BaseResponse<UserDto> 
                {
                    Message = "Login Successful",
                    Status =true,
                    Data = new UserDto{
                        UserName = (user.FirstName+ " " + user.LastName),
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Id = user.Id,
                        Gender = user.Gender,
                        Role = user.Role
                    }
                };
                return userLogin;
            }
            return  new BaseResponse<UserDto>
            {
                Message = "Incorrect email or password",
                Status = false
            } ;
        }

        // public BaseResponse<UserDto> Update(string id, UpdateUserRequestModels model)
        // {
        //     throw new NotImplementedException();
        // }
    }
}