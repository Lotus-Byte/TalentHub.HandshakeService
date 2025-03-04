using TalentHub.HandshakeService.Application.DTO;

namespace TalentHub.HandshakeService.Application.Interfaces;

public interface IHandshakeService
{
    Task<UserActionResponse> SendHandshakeAsync(SendHandshakeDto sendHandshakeDto);
    Task<IReadOnlyCollection<HandshakeDto>?> GetHandshakesBySenderAsync(Guid fromUserId);
    Task<IReadOnlyCollection<HandshakeDto>?> GetHandshakesByRecipientAsync(Guid fromUserId);
    Task<bool> DeleteHandshakeAsync(Guid handshakeId);
}
