using AutoMapper;
using TalentHub.InterdocService.Application.DTO.Interdoc;
using TalentHub.InterdocService.Infrastructure.Models;

namespace TalentHub.InterdocService.Application.Mapping;

public class InterdocMappingProfile : Profile
{
    public InterdocMappingProfile()
    {
        CreateMap<SendInterdocDto, Interdoc>()
            .ForMember(d => d.FromUserId, map => map.Ignore())
            .ForMember(d => d.ToUserId, map => map.Ignore())
            .ForMember(d => d.JobId, map => map.Ignore())
            .ForMember(d => d.ResumeId, map => map.Ignore())
            .ForMember(d => d.Created, map => map.Ignore())
            .ForMember(d => d.Deleted, map => map.Ignore());
    }
}