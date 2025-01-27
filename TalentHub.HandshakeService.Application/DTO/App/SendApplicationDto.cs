namespace TalentHub.HandshakeService.App.DTO.Application;

public class SendApplicationDto
{
    public Guid FromUserId { get; set; }
    public Guid ToUserId { get; set; }
    public Guid JobId { get; set; }
    public Guid ResumeId { get; set; }
    public string? Text { get; set; }
}