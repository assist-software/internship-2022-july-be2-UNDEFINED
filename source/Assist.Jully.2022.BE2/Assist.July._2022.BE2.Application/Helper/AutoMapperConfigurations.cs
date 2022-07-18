﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Assist.July._2022.BE2.Application.Helper
{
    public static class AutoMapperConfigurations
    {
        public static void AddCustomConfiguredAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            { cfg.AddProfile(new UserMapper()); }
            );
            var mapper=config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
