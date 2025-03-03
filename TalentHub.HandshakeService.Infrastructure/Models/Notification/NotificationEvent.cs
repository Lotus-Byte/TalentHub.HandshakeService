using TalentHub.HandshakeService.Infrastructure.Abstractions.DomainEvents;

namespace TalentHub.HandshakeService.Infrastructure.Models.Notification;

public class NotificationEvent : IDomainEvent
{
    public Guid UserId { get; set; }
    public required Notification Notification { get; set; }
    public DateTimeOffset Ts { get; set; }
}
