using TalentHub.InterdocService.Application.DTO.Interdoc;

namespace TalentHub.InterdocService.Application.Interfaces;

public interface IInterdocService
{
    Task<bool> SendInterdocAsync(SendInterdocDto createInterdocDto);
    Task<InterdocDto[]?> GetInterdocsBySenderAsync(Guid fromUserId);
    Task<bool> DeleteInterdocAsync(Guid interdocId);
}