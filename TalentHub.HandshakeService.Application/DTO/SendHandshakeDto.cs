namespace TalentHub.HandshakeService.Application.DTO;

public class SendHandshakeDto
{
    public Guid FromUserId { get; set; }
    public Guid ToUserId { get; set; }
    public required string ToUserRole { get; set; }
    public Guid JobId { get; set; }
    public Guid ResumeId { get; set; }
    public string? Text { get; set; }
}