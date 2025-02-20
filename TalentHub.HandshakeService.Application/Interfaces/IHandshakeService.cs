using TalentHub.HandshakeService.Application.DTO;

namespace TalentHub.HandshakeService.Application.Interfaces;

public interface IHandshakeService
{
    Task<bool> SendHandshakeAsync(SendHandshakeDto sendHandshakeDto);
    Task<HandshakeDto[]?> GetHandshakesBySenderAsync(Guid fromUserId);
    Task<HandshakeDto[]?> GetHandshakesByRecipientAsync(Guid fromUserId);
    Task<bool> DeleteHandshakeAsync(Guid handshakeId);
}