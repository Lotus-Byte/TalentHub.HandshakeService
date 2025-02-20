namespace TalentHub.HandshakeService.Api.Models;

public class SendHandshakeModel
{
    public required Guid FromUserId { get; set; }
    public required Guid ToUserId { get; set; }
    public required string ToUserRole { get; set; }
    public required Guid JobId { get; set; }
    public required Guid ResumeId { get; set; }
    public string? Text { get; set; }
}
