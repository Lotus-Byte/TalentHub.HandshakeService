using AutoMapper;
using TalentHub.HandshakeService.Application.DTO;
using TalentHub.HandshakeService.Infrastructure.Models;

namespace TalentHub.HandshakeService.Application.Mapping;

public class HandshakeMappingProfile : Profile
{
    public HandshakeMappingProfile()
    {
        CreateMap<SendHandshakeDto, Handshake>()
            .ForMember(d => d.HandshakeId, map => map.Ignore())
            .ForMember(d => d.Created, map => map.Ignore())
            .ForMember(d => d.Deleted, map => map.Ignore());
    }
}