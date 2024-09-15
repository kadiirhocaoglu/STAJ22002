using AutoMapper;
using HST.DTO.DTOs.CustomerAccountProcessDtos;
using HST.Entity.Entities;

namespace HST.Business.AutoMapper
{
    public class CustomerAccountProcessProfile : Profile
    {
        public CustomerAccountProcessProfile()
        {
            CreateMap<CustomerAccountProcessDto, CustomerAccountProcess>().ReverseMap();
        }
    }
}
