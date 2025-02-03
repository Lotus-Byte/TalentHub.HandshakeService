using AutoMapper;
using TalentHub.HandshakeService.Api.Models.Handshake;
using TalentHub.HandshakeService.Application.DTO.Handshake;

namespace TalentHub.HandshakeService.Api.Mapping;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<SendHandshakeModel, SendHandshakeDto>();
    }
}