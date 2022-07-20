using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Application.Dtos.ListingDtos;
using AutoMapper;

namespace Assist.July._2022.BE2.Application.Helper
{
    public class ListingProfileMapper : Profile
    {
        public ListingProfileMapper()
        {
            CreateMap<Listing, PostListingRequestDto>();
            CreateMap<PostListingRequestDto, Listing>();
            CreateMap<Listing, Listing>();
            CreateMap<PostListingRequestDto, Listing>()
                .ForAllMembers(entity => entity.Condition(
                    (src, dest, prop) =>
                    {
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string)
                            && string.IsNullOrEmpty((string)prop)) return false;
                        return true;
                    }));
            CreateMap<ListingDto, Listing>().ForMember(dest => dest.Location, act => act.Ignore());
        }
    }
}
