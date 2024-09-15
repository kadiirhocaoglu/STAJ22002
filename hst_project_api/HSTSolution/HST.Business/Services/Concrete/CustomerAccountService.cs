using AutoMapper;
using HST.Business.Services.Abstract;
using HST.DataAccess.UnitOfWork.Abstact;
using HST.DTO.DTOs.CustomerAccountDtos;
using HST.Entity.Entities;

namespace HST.Business.Services.Concrete
{
    public class CustomerAccountService : ICustomerAccountService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CustomerAccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<CustomerAccountDto>> GetAllCustomerAsync()
        {
            var customerAccounts = await unitOfWork.GetRepository<CustomerAccount>().GetAllAsync();
            var map = mapper.Map<List<CustomerAccountDto>>(customerAccounts);
            return map;
        }

        public async Task<List<CustomerAccount>> GetAllCustomerNoDtoAsync()
        {
            var customerAccounts = await unitOfWork.GetRepository<CustomerAccount>().GetAllAsync();
            return customerAccounts;
        }

        public async Task<CustomerAccountDto> GetCustomerAccountByIdAsync(int Id)
        {
            var customerAccount = await unitOfWork.GetRepository<CustomerAccount>().GetByIdAsync(Id);
            var map = mapper.Map<CustomerAccountDto>(customerAccount);
            return map;
        }

        public async Task<CustomerAccountDto> CreateCustomerAccountAsync(CreateCustomerAccountDto createCustomerAccountDto)
        {
            var customerAccount = mapper.Map<CustomerAccount>(createCustomerAccountDto);
            await unitOfWork.GetRepository<CustomerAccount>().AddAsync(customerAccount);
            await unitOfWork.SaveAsync();
            return mapper.Map<CustomerAccountDto>(customerAccount);
        }

        public async Task<bool> UpdateCustomerAccountAsync(int id, CreateCustomerAccountDto updateCustomerAccountDto)
        {
            var customerAccount = await unitOfWork.GetRepository<CustomerAccount>().GetByIdAsync(id);
            if (customerAccount == null)
            {
                return false;
            }

            customerAccount = mapper.Map(updateCustomerAccountDto, customerAccount);
            unitOfWork.GetRepository<CustomerAccount>().UpdateAsync(customerAccount);
            await unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteCustomerAccountAsync(int id)
        {
            var customerAccount = await unitOfWork.GetRepository<CustomerAccount>().GetByIdAsync(id);
            if (customerAccount == null)
            {
                return false;
            }

            unitOfWork.GetRepository<CustomerAccount>().DeleteAsync(customerAccount);
            await unitOfWork.SaveAsync();
            return true;
        }
    }
}
