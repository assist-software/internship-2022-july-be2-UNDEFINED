using Assist.July._2022.BE2.Application.Dtos.UserDtos;
using Assist.July._2022.BE2.Domain.Entities;
using AutoMapper;

namespace Assist.July._2022.BE2.Application.Helper
{
    public class UserMapper: Profile
    {
        public UserMapper()
        {
            CreateMap<LoginRequest, User>();
            CreateMap<RegisterRequest, User>();
            CreateMap<UpdateRequest, User>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string)
                    && string.IsNullOrEmpty((string)prop)) return false;
                    if (prop == "string") return false;
                    return true;
                }
            ));
            CreateMap<GoogleDto, User>();
        }
    }
}
