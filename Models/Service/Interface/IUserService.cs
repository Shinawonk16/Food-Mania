using Food_Mania.Models.Dtos;
using Food_Mania.Models.Enum;

namespace Models.Service.Interface
{
    public interface IUserService
    {
         BaseResponse<UserDto> Create(CreateUserRequestModel model);
        // BaseResponse<UserDto> Update(string id, UpdateUserRequestModels model);
        BaseResponse<UserDto> Get(string email, string password);
        BaseResponse<UserDto> GetById(string id);
        BaseResponse<UserDto> GetByRole(Role role);
        BaseResponse<IEnumerable<UserDto>> GetAll();
        BaseResponse<UserDto> Login(LoginUserRequestModel model);
    }
}