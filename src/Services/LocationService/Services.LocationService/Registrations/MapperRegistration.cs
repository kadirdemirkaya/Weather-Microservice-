﻿using BuildingBlock.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Services.LocationService.Registrations
{
    public static class MapperRegistration
    {
        public static IServiceCollection MapperServiceRegistration(this IServiceCollection services)
        {
            services.MapperServiceExtension(AssemblyReference.Assembly);

            return services;
        }
    }
}
