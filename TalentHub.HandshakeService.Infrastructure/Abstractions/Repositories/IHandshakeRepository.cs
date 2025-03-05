using TalentHub.HandshakeService.Infrastructure.Models;

namespace TalentHub.HandshakeService.Infrastructure.Abstractions.Repositories;

public interface IHandshakeRepository
{
    Task<bool> AddHandshakeAsync(Handshake? handshake);
    Task<Handshake[]> GetHandshakesByInitiatorAsync(Guid initiatorUserId);
    Task<Handshake[]> GetHandshakesByRecipientAsync(Guid recipientUserId);
    Task<bool> DeleteHandshakeAsync(Guid handshakeId);
}