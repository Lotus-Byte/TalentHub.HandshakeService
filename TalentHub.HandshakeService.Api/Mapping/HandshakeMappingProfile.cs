using AutoMapper;
using TalentHub.HandshakeService.Api.Models;
using TalentHub.HandshakeService.Application.DTO;

namespace TalentHub.HandshakeService.Api.Mapping;

public class HandshakeMappingProfile : Profile
{
    public HandshakeMappingProfile()
    {
        CreateMap<SendHandshakeModel, SendHandshakeDto>();
    }
}