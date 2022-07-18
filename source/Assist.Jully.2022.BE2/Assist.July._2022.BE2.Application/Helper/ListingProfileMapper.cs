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
        }
    }
}
