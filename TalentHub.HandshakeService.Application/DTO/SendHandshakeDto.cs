namespace TalentHub.HandshakeService.Application.DTO;

public class SendHandshakeDto
{
    public Guid InitiatorId { get; init; }
    public Guid RecipientId { get; init; }
    public required string InitiatorRole { get; init; }
    public Guid ItemId { get; init; }
}