using TalentHub.HandshakeService.Infrastructure.Models;

namespace TalentHub.HandshakeService.Infrastructure.Interfaces;

public interface IHandshakeRepository
{
    Task<bool> AddHandshakeAsync(Handshake? application);
    Task<Handshake[]> GetHandshakesBySenderAsync(Guid fromUserId);
    Task<Handshake[]> GetHandshakesByRecipientAsync(Guid toUserId);
    Task<bool> DeleteHandshakeAsync(Guid applicationId);
}