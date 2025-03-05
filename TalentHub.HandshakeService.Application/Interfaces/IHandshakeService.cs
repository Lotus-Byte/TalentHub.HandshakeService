using TalentHub.HandshakeService.Application.DTO;

namespace TalentHub.HandshakeService.Application.Interfaces;

public interface IHandshakeService
{
    Task<UserActionResponse> SendHandshakeAsync(SendHandshakeDto sendHandshakeDto);
    Task<IReadOnlyCollection<HandshakeDto>?> GetHandshakesByInitiatorAsync(Guid initiatorUserId);
    Task<IReadOnlyCollection<HandshakeDto>?> GetHandshakesByRecipientAsync(Guid recipientUserId);
    Task<bool> DeleteHandshakeAsync(Guid handshakeId);
}
