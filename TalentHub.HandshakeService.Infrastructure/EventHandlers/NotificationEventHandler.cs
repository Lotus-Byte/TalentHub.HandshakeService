using System.Text.Json;
using MassTransit;
using Microsoft.Extensions.Logging;
using TalentHub.HandshakeService.Infrastructure.Abstractions.DomainEvents;
using TalentHub.HandshakeService.Infrastructure.Models.Notification;

namespace TalentHub.HandshakeService.Infrastructure.EventHandlers;

public class NotificationEventHandler : IEventHandler<NotificationEvent>
{
    private const string MessageType = "urn:message:TalentHub.NotificationService.Host.Models:NotificationEventModel";
    
    private readonly IBus _bus;
    private readonly ILogger<NotificationEventHandler> _logger;

    public NotificationEventHandler(IBus bus, ILogger<NotificationEventHandler> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    public async Task HandleAsync(NotificationEvent notificationEvent)
    {
         var json = JsonSerializer.Serialize(notificationEvent);

         await _bus.Publish(notificationEvent, context =>
         {
             context.SupportedMessageTypes = [ MessageType ];
         });

        _logger.LogInformation($"""
                                Notification event published:
                                 {json}
                                """);
    }
}