using AutoMapper;
using HST.DTO.DTOs.ChatDtos;
using HST.Entity.Entities;


namespace HST.Business.AutoMapper
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<SendChatDto, Chat>().ReverseMap();
            CreateMap<ChatDto, Chat>().ReverseMap();
            CreateMap<SendChatDto, ChatDto>().ReverseMap();
        }
    }
}
