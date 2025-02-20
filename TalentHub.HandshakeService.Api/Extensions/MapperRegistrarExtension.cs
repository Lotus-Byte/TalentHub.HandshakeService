using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TalentHub.HandshakeService.Api.Mapping;

namespace TalentHub.HandshakeService.Api.Extensions;

public static class MapperRegistrarExtension
{
    public static IServiceCollection RegisterMapper(this IServiceCollection services)
    {
        services.AddSingleton<IMapper>(
            new Mapper(
                new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<HandshakeMappingProfile>();
                    cfg.AddProfile<Application.Mapping.HandshakeMappingProfile>();
                })));
        
        return services;
    }
}