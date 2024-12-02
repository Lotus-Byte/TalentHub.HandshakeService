using TalentHub.InterdocService.Infrastructure.Data;
using TalentHub.InterdocService.Infrastructure.Interfaces;
using TalentHub.InterdocService.Infrastructure.Models;

namespace TalentHub.InterdocService.Infrastructure.Repositories;

public class InterdocRepository : IInterdocRepository
{
    private readonly InterdocDbContext _context;
    
    public InterdocRepository(InterdocDbContext context) => _context = context;
    
    public async Task<bool> AddInterdocAsync(Interdoc? interdoc)
    {
        await _context.Interdocs.AddAsync(interdoc);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<IEnumerable<Interdoc>?> GetInterdocsByFromUserIdAsync(Guid fromUserId)
    {
        return await _context.Interdocs.FindAsync(fromUserId);
    }

    public async Task<bool> DeleteInterdocsByToUserIdAsync(Guid toUserId)
    {
        var interdocs = await _context.Interdocs.FindAsync(toUserId);
        
        if (interdocs is null) return false;
        
        foreach (var interdoc in interdocs)
			interdoc.Deleted = true;

        await _context.SaveChangesAsync();
        
        return true;
    }
}