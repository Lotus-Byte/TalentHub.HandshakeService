using TalentHub.HandshakeService.Infrastructure.Abstractions;
using TalentHub.HandshakeService.Infrastructure.Models.Notification;

namespace TalentHub.HandshakeService.Infrastructure.Providers;

public class NotificationEventFactory : INotificationEventFactory
{
    public NotificationEvent Create(Guid userId, Notification notification)
    {
        return new NotificationEvent
        {
            UserId = userId,
            Notification = notification,
            Ts = DateTimeOffset.Now
        };
    }
}