namespace TalentHub.HandshakeService.Application.DTO;

public class HandshakeDto
{
    public Guid HandshakeId { get; init; }
    public Guid SenderUserId { get; init; }
    public Guid ReceiverUserId { get; init; }
    public required string SenderRole { get; init; }
    public Guid ItemId { get; init; }
}
