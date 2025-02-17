using TalentHub.HandshakeService.Application.DTO.User;

namespace TalentHub.HandshakeService.Application.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetUserAsync(Guid userId);
}