using TalentHub.InterdocService.Infrastructure.Models;

namespace TalentHub.InterdocService.Infrastructure.Interfaces;

public interface IInterdocRepository
{
    Task<bool> AddInterdocAsync(Interdoc? interdoc);
    Task<Interdoc[]> GetInterdocsBySenderAsync(Guid fromUserId);
    Task<bool> DeleteInterdocAsync(Guid interdocId);
}