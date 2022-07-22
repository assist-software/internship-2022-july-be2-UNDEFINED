using AutoMapper;
using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Application.Dtos.MessageDtos;

namespace Assist.July._2022.BE2.Application.Helper
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<PostMessageDto, Message>();
            CreateMap<Message, PostMessageDto>();
            CreateMap<MessageDto, Message>();
            CreateMap<Message, MessageDto>();
        }
    }
}
