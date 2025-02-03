using AutoMapper;
using TalentHub.HandshakeService.Application.DTO.Handshake;
using TalentHub.HandshakeService.Infrastructure.Models;

namespace TalentHub.HandshakeService.Application.Mapping;

public class HandshakeMappingProfile : Profile
{
    public HandshakeMappingProfile()
    {
        CreateMap<SendHandshakeDto, Handshake>()
            .ForMember(d => d.FromUserId, map => map.Ignore())
            .ForMember(d => d.ToUserId, map => map.Ignore())
            .ForMember(d => d.JobId, map => map.Ignore())
            .ForMember(d => d.ResumeId, map => map.Ignore())
            .ForMember(d => d.Created, map => map.Ignore())
            .ForMember(d => d.Deleted, map => map.Ignore());
    }
}