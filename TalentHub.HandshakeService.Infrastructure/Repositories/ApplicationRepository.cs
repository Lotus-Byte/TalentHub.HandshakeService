using Microsoft.EntityFrameworkCore;
using TalentHub.HandshakeService.Infrastructure.Data;
using TalentHub.HandshakeService.Infrastructure.Interfaces;
using TalentHub.HandshakeService.Infrastructure.Models;

namespace TalentHub.HandshakeService.Infrastructure.Repositories;

public class ApplicationRepository : Interfaces.IApplicationRepository
{
    private readonly HandshakeDbContext _context;
    
    public ApplicationRepository(HandshakeDbContext context) => _context = context;
    
    public async Task<bool> AddApplicationAsync(Application? application)
    {
        if (application is null) return false;
        await _context.Applications.AddAsync(application);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<Application[]> GetApplicationsBySenderAsync(Guid fromUserId)
    {
        return await _context.Applications.Where(e => e.FromUserId == fromUserId).ToArrayAsync();
    }

    public async Task<Application[]> GetApplicationsByRecipientAsync(Guid toUserId)
    {
        return await _context.Applications.Where(e => e.ToUserId == toUserId).ToArrayAsync();
    }

    public async Task<bool> DeleteApplicationAsync(Guid applicationId)
    {
        var application = await _context.Applications.FindAsync(applicationId);
        
        if (application is null) return false;
        
        application.Deleted = true;

        await _context.SaveChangesAsync();
        
        return true;
    }
}
