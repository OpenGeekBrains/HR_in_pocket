using System;
using System.Linq;

using AutoMapper;

using HRInPocket.Domain.AutoMapperProfiles;

using Microsoft.Extensions.DependencyInjection;

namespace HRInPocket.Domain
{
    public static class Registrator
    {
        public static IServiceCollection AddAutoMapperWithProfiles(this IServiceCollection services, params Type[] profiles)
        {
            var profilesToInject = new[] {typeof(MappingDTOProfile)};
            return services.AddAutoMapper(profiles.Length > 0 ? profiles.Concat(profilesToInject).ToArray() : profilesToInject);
        }
    }
}