using AutoMapper;
using TalentHub.HandshakeService.App.DTO.Application;
using TalentHub.HandshakeService.Infrastructure.Models;

namespace TalentHub.HandshakeService.App.Mapping;

public class HandshakeMappingProfile : Profile
{
    public HandshakeMappingProfile()
    {
        CreateMap<SendApplicationDto, Application>()
            .ForMember(d => d.FromUserId, map => map.Ignore())
            .ForMember(d => d.ToUserId, map => map.Ignore())
            .ForMember(d => d.JobId, map => map.Ignore())
            .ForMember(d => d.ResumeId, map => map.Ignore())
            .ForMember(d => d.Created, map => map.Ignore())
            .ForMember(d => d.Deleted, map => map.Ignore());
    }
}