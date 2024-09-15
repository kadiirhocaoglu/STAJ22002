using HST.DTO.DTOs.CustomerAccountDtos;
using HST.Entity.Entities;

namespace HST.Business.Services.Abstract
{
    public interface ICustomerAccountService
    {
        Task<List<CustomerAccountDto>> GetAllCustomerAsync();
        Task<List<CustomerAccount>> GetAllCustomerNoDtoAsync();
        Task<CustomerAccountDto> GetCustomerAccountByIdAsync(int Id);
        Task<CustomerAccountDto> CreateCustomerAccountAsync(CreateCustomerAccountDto createCustomerAccountDto);
        Task<bool> UpdateCustomerAccountAsync(int id, CreateCustomerAccountDto updateCustomerAccountDto);
        Task<bool> DeleteCustomerAccountAsync(int id);
    }
}
