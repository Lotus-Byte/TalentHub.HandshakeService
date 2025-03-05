using TalentHub.HandshakeService.Application.DTO;

namespace TalentHub.HandshakeService.Application.Interfaces;

public interface IUserService
{
    Task<UserContactsInfo> GetUserContactsAsync(Guid userId);
}