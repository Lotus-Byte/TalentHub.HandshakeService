using AutoMapper;
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
    public HandshakeService(IMapper mapper, IUserService userService, IHandshakeRepository repository, INotificationEventFactory eventFactory, IEventHandler<NotificationEvent> eventHandler)
    {
        _mapper = mapper;
        _repository = repository;
        _userService = userService;
        _eventFactory = eventFactory;
        _eventHandler = eventHandler;
    }
    
    public async Task<(bool, object?)> SendHandshakeAsync(SendHandshakeDto sendHandshakeDto)
    {
        var handshake = _mapper.Map<Handshake>(sendHandshakeDto);

        bool success = await _repository.AddHandshakeAsync(handshake);
        if (! success)
            return (false, null);
        

        object? obj;
        if (sendHandshakeDto.SenderRole == "Employer")
        {
            var personDto = await _userService.GetPersonAsync(sendHandshakeDto.ReceiverUserId);
            if (personDto != null)
            {
                var notificationEvent = _eventFactory.Create(
                    sendHandshakeDto.SenderUserId,
                    new Notification
                    {
                        Title = "Handshake sent",
                        Content = $"Handshake sent to '{personDto.FirstName} {personDto.LastName}'",
                    });
                await _eventHandler.HandleAsync(notificationEvent);
                obj = personDto;
            }
            else
            {
                success = false;
                obj = null;
            }
        }
        else
            obj = null;

        return (success, obj);
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