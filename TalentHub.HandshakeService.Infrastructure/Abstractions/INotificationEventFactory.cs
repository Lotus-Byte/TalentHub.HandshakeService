using TalentHub.HandshakeService.Infrastructure.Models.Notification;

namespace TalentHub.HandshakeService.Infrastructure.Abstractions;

public interface INotificationEventFactory
{
    NotificationEvent Create(Guid userId, Notification notification);
}