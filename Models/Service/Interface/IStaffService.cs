using Food_Mania.Models.Dtos;
using Food_Mania.Models.Enum;

namespace Food_Mania.Models.Service.Interface
{
    public interface IStaffService
    {
        BaseResponse<StaffDto> Create(CreateStaffRequestModel model);
        BaseResponse<StaffDto> Update(string id, UpdateStaffRequestModel model);
        BaseResponse<StaffDto> Get(string email, string password);
        BaseResponse<StaffDto> GetById(string id);
        BaseResponse<StaffDto> Delete(string id);
        BaseResponse<StaffDto> GetByRole(Role role);
        BaseResponse<IEnumerable<StaffDto>> GetAll();
        BaseResponse<StaffDto> VeiwProfile(string id);
    }
}