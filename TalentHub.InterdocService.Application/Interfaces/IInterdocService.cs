using TalentHub.InterdocService.Application.DTO.Interdoc;

namespace TalentHub.InterdocService.Application.Interfaces;

public interface IInterdocService
{
    Task<bool> SendInterdocAsync(SendInterdocDto user);
    Task<InterdocDto?> GetInterdocByFromUserIdAsync(Guid fromUserId);
    Task<bool> DeleteInterdocByToUserIdAsync(Guid toUserId);
}