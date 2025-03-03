using TalentHub.HandshakeService.Infrastructure.Models;

namespace TalentHub.HandshakeService.Infrastructure.Abstractions.Repositories;

public interface IHandshakeRepository
{
    Task<bool> AddHandshakeAsync(Handshake? handshake);
    Task<Handshake[]> GetHandshakesBySenderAsync(Guid fromUserId);
    Task<Handshake[]> GetHandshakesByRecipientAsync(Guid toUserId);
    Task<bool> DeleteHandshakeAsync(Guid handshakeId);
}