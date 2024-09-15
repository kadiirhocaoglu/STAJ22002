using AutoMapper;
using HST.DTO.DTOs.CustomerAccountDtos;
using HST.Entity.Entities;


namespace HST.Business.AutoMapper
{
    public class CustomerAccountProfile : Profile
    {
        public CustomerAccountProfile()
        {
            CreateMap<CustomerAccountDto, CustomerAccount>().ReverseMap();
            CreateMap<CreateCustomerAccountDto, CustomerAccount>().ReverseMap();

        }
    }
}
