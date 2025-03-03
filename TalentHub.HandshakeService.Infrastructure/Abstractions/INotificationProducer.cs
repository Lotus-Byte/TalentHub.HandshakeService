using TalentHub.HandshakeService.Infrastructure.Models.Notification;

namespace TalentHub.HandshakeService.Infrastructure.Abstractions;

public interface INotificationProducer
{
    Task SendAsync(NotificationEvent @event);
}