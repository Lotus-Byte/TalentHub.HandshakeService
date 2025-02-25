using TalentHub.HandshakeService.Application.DTO;

namespace TalentHub.HandshakeService.Application.Interfaces;

public interface IHandshakeService
{
    // TODO: CancellationToken
    Task<bool> SendHandshakeAsync(SendHandshakeDto sendHandshakeDto);
    // TODO: лучше возвращать IReadOnlyCollection
    Task<HandshakeDto[]?> GetHandshakesBySenderAsync(Guid fromUserId);
    Task<HandshakeDto[]?> GetHandshakesByRecipientAsync(Guid fromUserId);
    Task<bool> DeleteHandshakeAsync(Guid handshakeId);
}
