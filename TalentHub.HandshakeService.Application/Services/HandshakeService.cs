using System.Diagnostics;
using AutoMapper;
using TalentHub.HandshakeService.Application.Dictionaries;
using TalentHub.HandshakeService.Application.DTO;
using TalentHub.HandshakeService.Application.Interfaces;
using TalentHub.HandshakeService.Infrastructure.Abstractions;
using TalentHub.HandshakeService.Infrastructure.Abstractions.DomainEvents;
using TalentHub.HandshakeService.Infrastructure.Abstractions.Repositories;
using TalentHub.HandshakeService.Infrastructure.Models;
using TalentHub.HandshakeService.Infrastructure.Models.Notification;

namespace TalentHub.HandshakeService.Application.Services;

public class HandshakeService : IHandshakeService
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly IHandshakeRepository _repository;
    private readonly INotificationEventFactory _eventFactory;
    private readonly IEventHandler<NotificationEvent> _eventHandler;
    public HandshakeService(
        IMapper mapper, 
        IUserService userService, 
        IHandshakeRepository repository, 
        INotificationEventFactory eventFactory, 
        IEventHandler<NotificationEvent> eventHandler)
    {
        _mapper = mapper;
        _repository = repository;
        _userService = userService;
        _eventFactory = eventFactory;
        _eventHandler = eventHandler;
    }
    
    public async Task<UserActionResponse> SendHandshakeAsync(SendHandshakeDto sendHandshakeDto)
    {
        var success = await _repository.AddHandshakeAsync(_mapper.Map<Handshake>(sendHandshakeDto));
        
        if (!success) return UserActionResponse.Failure("Operation failed.");

        switch (sendHandshakeDto.InitiatorRole)
        {
            case UserRole.Employer:
            {
                var contactsInfo = await _userService.GetUserContactsAsync(sendHandshakeDto.RecipientId);
                
                return UserActionResponse.SuccessWithContacts(contactsInfo);
            }
            case UserRole.Person:
            {
                var notificationEvent = _eventFactory.Create(
                    sendHandshakeDto.InitiatorId,
                    new Notification
                    {
                        Title = "Handshake sent",
                        Content = $"Handshake sent to '{sendHandshakeDto.RecipientId}'",
                    });
                
                await _eventHandler.HandleAsync(notificationEvent);
                
                return UserActionResponse.SuccessWithMessage(
                    "You have successfully applied for the position. " +
                    "A company representative will contact you soon.");
            }
            default:
            {
                return UserActionResponse.Failure("Unsupported user role.");
            }
        }
    }

    public async Task<IReadOnlyCollection<HandshakeDto>?> GetHandshakesBySenderAsync(Guid fromUserId)
    {
        var applications = await _repository.GetHandshakesBySenderAsync(fromUserId);
        
        if (applications == null) return null;
        
        var applicationsDto = _mapper.Map<Handshake[], HandshakeDto[]>(applications);
        
        return applicationsDto;
    }

    public async Task<IReadOnlyCollection<HandshakeDto>?> GetHandshakesByRecipientAsync(Guid toUserId)
    {
        var applications = await _repository.GetHandshakesBySenderAsync(toUserId);

        if (applications == null) return null;

        var applicationsDto = _mapper.Map<Handshake[], HandshakeDto[]>(applications);

        return applicationsDto;
    }

    public async Task<bool> DeleteHandshakeAsync(Guid applicationId)
    {
        return await _repository.DeleteHandshakeAsync(applicationId);
    }
}