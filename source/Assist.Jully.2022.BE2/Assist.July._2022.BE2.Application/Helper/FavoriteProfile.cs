using Assist.July._2022.BE2.Domain.Entities;
using Assist.July._2022.BE2.Application.Dtos.FavoriteDtos;
using AutoMapper;

namespace Assist.July._2022.BE2.Application.Helper
{
    internal class FavoriteProfile : Profile
    {
        public FavoriteProfile()
        {
            CreateMap<Favorite, FavoriteDto>();
            CreateMap<FavoriteDto, Favorite>();
            CreateMap<Favorite, Favorite>();
        }
    }
}
