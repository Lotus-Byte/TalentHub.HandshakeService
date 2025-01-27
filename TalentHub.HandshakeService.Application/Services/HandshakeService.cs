using AutoMapper;
using TalentHub.HandshakeService.App.DTO.Application;
using TalentHub.HandshakeService.App.Interfaces;
using TalentHub.HandshakeService.Infrastructure.Interfaces;
using TalentHub.HandshakeService.Infrastructure.Models;

namespace TalentHub.HandshakeService.App.Services;

public class HandshakeService : IHandshakeService
{
    private readonly IMapper _mapper;
    private readonly IApplicationRepository _repository;

    public HandshakeService(IApplicationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<bool> SendApplicationAsync(SendApplicationDto sendApplicationDto)
    {
        var application = _mapper.Map<SendApplicationDto, Application>(sendApplicationDto); 
        
        return await _repository.AddApplicationAsync(application);
    }

    public async Task<ApplicationDto[]?> GetApplicationsBySenderAsync(Guid fromUserId)
    {
        var applications = await _repository.GetApplicationsBySenderAsync(fromUserId);
        
        if (applications == null) return null;
        
        var applicationsDto = _mapper.Map<Application[], ApplicationDto[]>(applications);
        
        return applicationsDto;
    }

    public async Task<ApplicationDto[]?> GetApplicationsByRecipientAsync(Guid toUserId)
    {
        var applications = await _repository.GetApplicationsBySenderAsync(toUserId);

        if (applications == null) return null;

        var applicationsDto = _mapper.Map<Application[], ApplicationDto[]>(applications);

        return applicationsDto;
    }

    public async Task<bool> DeleteApplicationAsync(Guid applicationId)
    {
        return await _repository.DeleteApplicationAsync(applicationId);
    }
}