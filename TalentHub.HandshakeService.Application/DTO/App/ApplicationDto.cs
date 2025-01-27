namespace TalentHub.HandshakeService.App.DTO.Application;

public class ApplicationDto
{
    public Guid ApplicationId { get; set; }
    public Guid FromUserId { get; set; }
    public Guid ToUserId { get; set; }
    public Guid JobId { get; set; }
    public Guid ResumeId { get; set; }
    public string? Text { get; set; }
}
