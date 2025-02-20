using TalentHub.HandshakeService.Application.DTO;

namespace TalentHub.HandshakeService.Application.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetUserAsync(SendHandshakeDto sendHandShakeDto);
}