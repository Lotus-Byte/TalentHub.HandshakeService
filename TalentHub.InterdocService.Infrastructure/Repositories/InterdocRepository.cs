using Microsoft.EntityFrameworkCore;
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
        if (interdoc is null) return false;
        await _context.Interdocs.AddAsync(interdoc);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<Interdoc[]> GetInterdocsBySenderAsync(Guid fromUserId)
    {
        return await _context.Interdocs.Where(e => e.FromUserId == fromUserId).ToArrayAsync();
    }

    public async Task<bool> DeleteInterdocAsync(Guid interdocId)
    {
        var interdoc = await _context.Interdocs.FindAsync(interdocId);
        
        if (interdoc is null) return false;
        
        interdoc.Deleted = true;

        await _context.SaveChangesAsync();
        
        return true;
    }
}