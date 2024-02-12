﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Services.ClientAndServerService.Registrations
{
    public static class MediatrRegistration
    {
        public static IServiceCollection MediatrServiceRegistration(this IServiceCollection services)
        {
            services.AddMediatR(AssemblyReference.Assembly);

            return services;
        }
    }
}
