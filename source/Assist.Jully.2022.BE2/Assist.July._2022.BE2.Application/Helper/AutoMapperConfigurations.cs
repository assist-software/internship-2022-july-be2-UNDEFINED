using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Assist.July._2022.BE2.Application.Mapper;

namespace Assist.July._2022.BE2.Application.Helper
{
    public static class AutoMapperConfigurations
    {
        public static void AddCustomConfiguredAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            { 
            cfg.AddProfile(new UserMapper()); 
            cfg.AddProfile(new ListingProfileMapper());
            cfg.AddProfile(new MessageProfile());
            }
            );
            var mapper=config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
