namespace TalentHub.InterdocService.Infrastructure.Models;

public class Interdoc
{
    public Guid InterdocId { get; set; }
    public Guid FromUserId { get; set; }
    public Guid ToUserId { get; set; }
    public Guid JobId { get; set; }
    public Guid ResumeId { get; set; }
    public string Text { get; set; }
    public DateTime Created { get; set; }
    public bool Deleted { get; set; }
}