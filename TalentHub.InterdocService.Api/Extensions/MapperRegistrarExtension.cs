using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TalentHub.InterdocService.Api.Mapping;

namespace TalentHub.InterdocService.Api.Extensions;

public static class MapperRegistrarExtension
{
    public static IServiceCollection RegisterMapper(this IServiceCollection services)
    {
        services.AddSingleton<IMapper>(
            new Mapper(
                new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<InterdocMappingProfile>();
                    cfg.AddProfile<TalentHub.InterdocService.Application.Mapping.InterdocMappingProfile>();
                })));
        
        return services;
    }
}