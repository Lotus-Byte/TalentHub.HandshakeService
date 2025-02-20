namespace TalentHub.HandshakeService.Infrastructure.Models;

public class Handshake
{
    public Guid HandshakeId { get; set; }
    public Guid FromUserId { get; set; }
    public Guid ToUserId { get; set; }
    public Guid JobId { get; set; }
    public Guid ResumeId { get; set; }
    public string? Text { get; set; }
    public DateTime Created { get; set; }
    public bool Deleted { get; set; }
}