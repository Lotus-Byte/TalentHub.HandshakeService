using TalentHub.InterdocService.Infrastructure.Models;

namespace TalentHub.InterdocService.Infrastructure.Interfaces;

public interface IInterdocRepository
{
    Task<bool> AddInterdocAsync(Interdoc? interdoc);
    Task<IEnumerable<Interdoc>> GetInterdocsByFromUserIdAsync(Guid fromUserId);
    Task<bool> DeleteInterdocsByToUserIdAsync(Guid toUserId);
}