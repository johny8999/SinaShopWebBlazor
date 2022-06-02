﻿using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SinaShop.Application.Contract.Mappings;

namespace SinaShop.Application.Comman.ExMethod
{
    public static class AutoMapperEx
    {
        public static void AddCustomAutoMapper(this IServiceCollection services)
        {
            var ProfilesAssemblies = typeof(UserProfile).Assembly.GetTypes()
                .Where(a => a != typeof(Profile) && typeof(Profile).IsAssignableFrom(a));

            services.AddAutoMapper(a => a.AddProfiles(ProfilesAssemblies
                .Select(b => (Profile)Activator.CreateInstance(b))));
        }
    }
}
