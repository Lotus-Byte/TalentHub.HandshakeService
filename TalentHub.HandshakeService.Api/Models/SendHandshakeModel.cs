namespace TalentHub.HandshakeService.Api.Models;

public class SendHandshakeModel
{
    public required Guid SenderUserId { get; init; }
    public required Guid ReceiverUserId { get; init; }
    public required string SenderRole { get; init; }
    public required Guid ItemId { get; init; }
}
