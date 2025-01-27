using AutoMapper;
using TalentHub.HandshakeService.Api.Models.Application;
using TalentHub.HandshakeService.App.DTO.Application;

namespace TalentHub.HandshakeService.Api.Mapping;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateMap<SendApplicationModel, SendApplicationDto>();
    }
}