using Microsoft.EntityFrameworkCore;
using TalentHub.HandshakeService.Infrastructure.Abstractions.Repositories;
using TalentHub.HandshakeService.Infrastructure.Data;
using TalentHub.HandshakeService.Infrastructure.Models;

namespace TalentHub.HandshakeService.Infrastructure.Repositories;

public class HandshakeRepository : IHandshakeRepository
{
    private readonly HandshakeDbContext _context;
    
    public HandshakeRepository(HandshakeDbContext context) => _context = context;
    
    public async Task<bool> AddHandshakeAsync(Handshake? handshake)
    {
        if (handshake is null) return false;
        
        await _context.Handshakes.AddAsync(handshake);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<Handshake[]> GetHandshakesByInitiatorAsync(Guid initiatorUserId)
    {
        return await _context.Handshakes.Where(e => e.InitiatorId == initiatorUserId).ToArrayAsync();
    }

    public async Task<Handshake[]> GetHandshakesByRecipientAsync(Guid recipientUserId)
    {
        return await _context.Handshakes.Where(e => e.RecipientId == recipientUserId).ToArrayAsync();
    }

    public async Task<bool> DeleteHandshakeAsync(Guid handshakeId)
    {
        var handshake = await _context.Handshakes.FindAsync(handshakeId);
        
        if (handshake is null) return false;
        
        handshake.Deleted = true;

        await _context.SaveChangesAsync();
        
        return true;
    }
}
