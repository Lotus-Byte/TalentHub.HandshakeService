namespace TalentHub.InterdocService.Application.DTO.Interdoc;

public class InterdocDto
{
    public Guid InterdocId { get; set; }
    public Guid FromUserId { get; set; }
    public Guid ToUserId { get; set; }
    public Guid JobId { get; set; }
    public Guid ResumeId { get; set; }
    public string? Text { get; set; }
}