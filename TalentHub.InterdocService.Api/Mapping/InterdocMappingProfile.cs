using AutoMapper;
using TalentHub.InterdocService.Api.Models.Interdoc;
using TalentHub.InterdocService.Application.DTO.Interdoc;

namespace TalentHub.InterdocService.Api.Mapping;

public class InterdocMappingProfile : Profile
{
    public InterdocMappingProfile()
    {
        CreateMap<SendInterdocModel, SendInterdocDto>();
    }
}