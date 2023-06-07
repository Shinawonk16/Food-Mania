using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Food_Mania.Models.Dtos;
using Food_Mania.Models.Entities;
using Food_Mania.Models.Enum;
using Food_Mania.Models.Repository.Interface;
using Food_Mania.Models.Service.Interface;

namespace Food_Mania.Models.Service.Implementation
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;

        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        public BaseResponse<StaffDto> Create(CreateStaffRequestModel model)
        {
            var check = _staffRepository.CheckIfEmailExist(model.Email);
            if (check == true)
            {
                return new BaseResponse<StaffDto>
                {
                    Message = "Email Already Exist",
                    Status = false
                };
            }
            // var hash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var staff = new Staff
            {
                CreatedAt = model.CreatedAt,
                DateOfBirth = model.DathOfBirth,
                User = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Gender = model.Gender,
                    UserName = (model.FirstName + model.LastName),
                    Password = model.Password,
                    CreatedAt = model.CreatedAt,
                    // UpdatedAt = DateTime.Now,
                    Role = model.Role,

                }
            };
            _staffRepository.Create(staff);
            _staffRepository.Save();
            return new BaseResponse<StaffDto>
            {
                Message = "Successfully Registered",
                Status = true,
                Data = new StaffDto
                {
                    Gender = model.Gender,
                    User = new UserDto
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                    }
                }

            };
        }

        public BaseResponse<StaffDto> Get(string email, string password)
        {
            var customer = _staffRepository.Get(e => e.User.Email == email);
            if (customer != null && customer.User.Password == password)
            {
                return new BaseResponse<StaffDto>()
                {
                    Message = "Found",
                    Status = true,
                    Data = new StaffDto
                    {
                        User = new UserDto
                        {
                            Id = customer.Id,
                            UserName = $"{customer.User.LastName} {customer.User.FirstName}",
                            Email = customer.User.Email,
                            PhoneNumber = customer.User.PhoneNumber,
                            Gender = customer.User.Gender,
                        }
                    }
                };
            }
            return new BaseResponse<StaffDto>
            {
                Message = "Not  Found",
                Status = false
            };
        }

        public BaseResponse<IEnumerable<StaffDto>> GetAll()
        {
            var all = _staffRepository.GetAllStaff();
            return new BaseResponse<IEnumerable<StaffDto>>
            {
                Message = "Successful",
                Status = true,
                Data = all.Select(x => new StaffDto
                {
                    CreatedAt = x.CreatedAt,
                    DathOfBirth = x.DateOfBirth,
                    User = new UserDto
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

        public BaseResponse<StaffDto> GetById(string id)
        {
            var get = _staffRepository.Get(i => i.User.Id == id);
            if (get is not null)
            {
                return new BaseResponse<StaffDto>()
                {
                    Message = "Credentials found",
                    Status = true,
                    Data = new StaffDto
                    {
                        DathOfBirth = get.DateOfBirth,
                        User = new UserDto
                        {
                            FirstName = get.User.FirstName,
                            LastName = get.User.LastName,
                            Gender = get.User.Gender,
                            Id = get.Id,
                            UserName = $"{get.User.LastName} {get.User.FirstName}",
                            Email = get.User.Email,
                            PhoneNumber = get.User.PhoneNumber,
                            Role = get.User.Role
                        }

                    }
                };
            }
            return new BaseResponse<StaffDto>()
            {
                Message = "Not Found",
                Status = false,
            };
        }

        public BaseResponse<StaffDto> GetByRole(Role role)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<StaffDto> Update(string id, UpdateStaffRequestModel model)
        {
            var staff = _staffRepository.Get(x => x.Id == id);
            if (staff == null)
            {
                return new BaseResponse<StaffDto>
                {
                    Message = "Account Not Found",
                    Status = false
                };
            }
            staff.User.FirstName = model.FirstName;
            staff.User.LastName = model.LastName;
            staff.User.PhoneNumber = model.PhoneNumber;
            staff.User.Email = model.Email;
            staff.User.Role = model.Role;
            staff.User.UserName = (model.FirstName + model.LastName);
            var response = _staffRepository.Update(staff);
            return new BaseResponse<StaffDto>
            {
                Message = "Account Updated Successfully",
                Status = true,
                Data = new StaffDto
                {
                    UpdatedAt = model.UpdatedAt,
                    User = new UserDto
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        Role = model.Role,
                        UserName = (model.FirstName + model.LastName)
                    }
                    
                   
                }


            };
        }
        public BaseResponse<StaffDto> Delete(string id)
        {
            var staff = _staffRepository.Get(x => x.Id == id);
            if (staff == null)
            {
                return new BaseResponse<StaffDto>
                {
                    Message = "Account Not Found",
                    Status = false
                };
            }
            staff.IsDeleted = true;
            staff.UpdatedAt = DateTime.Now;
            _staffRepository.Update(staff);
            return new BaseResponse<StaffDto>
            {
                Message = "Account deleted Successfully",
                Status = true,
            };
        }

        public BaseResponse<StaffDto> VeiwProfile(string id)
        {
            var profile = _staffRepository.Get(a => a.Id == id);
            if (profile == null)
            {
                return new BaseResponse<StaffDto>
                {
                    Message = "Profile not found",
                    Status = false
                };
            }

            return new BaseResponse<StaffDto>
            {
                Message = "Profile was found successfully ",
                Status = true,
                Data = new StaffDto
                {
                    User = new UserDto()
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