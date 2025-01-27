using TalentHub.HandshakeService.App.DTO.Application;

namespace TalentHub.HandshakeService.App.Interfaces;

public interface IHandshakeService
{
    Task<bool> SendApplicationAsync(SendApplicationDto sendApplicationDto);
    Task<ApplicationDto[]?> GetApplicationsBySenderAsync(Guid fromUserId);
    Task<ApplicationDto[]?> GetApplicationsByRecipientAsync(Guid fromUserId);
    Task<bool> DeleteApplicationAsync(Guid applicationId);
}