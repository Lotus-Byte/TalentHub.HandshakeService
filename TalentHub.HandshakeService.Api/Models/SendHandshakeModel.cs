namespace TalentHub.HandshakeService.Api.Models;

public class SendHandshakeModel
{
    public required Guid InitiatorId { get; init; }
    public required Guid RecipientId { get; init; }
    public required string InitiatorRole { get; init; }
    public required Guid ItemId { get; init; }
}
