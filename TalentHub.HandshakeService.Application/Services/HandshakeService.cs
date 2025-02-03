using AutoMapper;
using TalentHub.HandshakeService.Application.DTO.Handshake;
using TalentHub.HandshakeService.Application.Interfaces;
using TalentHub.HandshakeService.Infrastructure.Interfaces;
using TalentHub.HandshakeService.Infrastructure.Models;

namespace TalentHub.HandshakeService.Application.Services;

public class HandshakeService : IHandshakeService
{
    private readonly IMapper _mapper;
    private readonly IHandshakeRepository _repository;

    public HandshakeService(IHandshakeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<bool> SendHandshakeAsync(SendHandshakeDto sendApplicationDto)
    {
        var application = _mapper.Map<SendHandshakeDto, Handshake>(sendApplicationDto); 
        
        return await _repository.AddHandshakeAsync(application);
    }

    public async Task<HandshakeDto[]?> GetHandshakesBySenderAsync(Guid fromUserId)
    {
        var applications = await _repository.GetHandshakesBySenderAsync(fromUserId);
        
        if (applications == null) return null;
        
        var applicationsDto = _mapper.Map<Infrastructure.Models.Handshake[], HandshakeDto[]>(applications);
        
        return applicationsDto;
    }

    public async Task<HandshakeDto[]?> GetHandshakesByRecipientAsync(Guid toUserId)
    {
        var applications = await _repository.GetHandshakesBySenderAsync(toUserId);

        if (applications == null) return null;

        var applicationsDto = _mapper.Map<Infrastructure.Models.Handshake[], HandshakeDto[]>(applications);

        return applicationsDto;
    }

    public async Task<bool> DeleteHandshakeAsync(Guid applicationId)
    {
        return await _repository.DeleteHandshakeAsync(applicationId);
    }
}