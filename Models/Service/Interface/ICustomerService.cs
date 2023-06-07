using Food_Mania.Models.Dtos;

namespace Food_Mania.Models.Service.Interface
{
    public interface ICustomerService
    {
        BaseResponse<CustomerDto> CheckWallet(string id);
        // BaseResponse<CustomerDto> Delete(string email);
        BaseResponse<CustomerDto> Update(string id, UpdateCustomerRequestModel model);
        // BaseResponse<CustomerDto> Get(string email, string password);
        BaseResponse<CustomerDto> GetById(string id);
        BaseResponse<CustomerDto> FundWallet(string id, decimal amount);
        BaseResponse<IEnumerable<CustomerDto>> GetAll();
        BaseResponse<CustomerDto> Create(CreateCustomerRequestModel model);
        BaseResponse<CustomerDto> UpdateWallet(string id, UpdateWalletRequestModel model);
        BaseResponse<CustomerDto> VeiwProfile(string email);
        BaseResponse<IEnumerable<CustomerDto>> GetSelected(string city);



    }
}