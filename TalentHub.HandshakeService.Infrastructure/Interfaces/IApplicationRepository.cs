using TalentHub.HandshakeService.Infrastructure.Models;

namespace TalentHub.HandshakeService.Infrastructure.Interfaces;

public interface IApplicationRepository
{
    Task<bool> AddApplicationAsync(Application? application);
    Task<Application[]> GetApplicationsBySenderAsync(Guid fromUserId);
    Task<Application[]> GetApplicationsByRecipientAsync(Guid fromUserId);
    Task<bool> DeleteApplicationAsync(Guid applicationId);
}