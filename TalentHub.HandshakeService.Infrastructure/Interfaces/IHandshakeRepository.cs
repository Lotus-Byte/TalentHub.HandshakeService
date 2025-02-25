using TalentHub.HandshakeService.Infrastructure.Models;

namespace TalentHub.HandshakeService.Infrastructure.Interfaces;

public interface IHandshakeRepository
{
    // TODO: CancellationToken на всех методах
    Task<bool> AddHandshakeAsync(Handshake? application);
    Task<Handshake[]> GetHandshakesBySenderAsync(Guid fromUserId);
    Task<Handshake[]> GetHandshakesByRecipientAsync(Guid toUserId);
    Task<bool> DeleteHandshakeAsync(Guid applicationId);
}
