namespace TalentHub.HandshakeService.Application.DTO;

public class SendHandshakeDto
{
    public Guid SenderUserId { get; init; }
    public Guid ReceiverUserId { get; init; }
    public required string SenderRole { get; init; }
    public Guid ItemId { get; init; }
}