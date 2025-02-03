namespace TalentHub.HandshakeService.Application.DTO.Handshake;

public class HandshakeDto
{
    public Guid ApplicationId { get; set; }
    public Guid FromUserId { get; set; }
    public Guid ToUserId { get; set; }
    public Guid JobId { get; set; }
    public Guid ResumeId { get; set; }
    public string? Text { get; set; }
}
