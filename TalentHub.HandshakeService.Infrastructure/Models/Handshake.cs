namespace TalentHub.HandshakeService.Infrastructure.Models;

public class Handshake
{
    public Guid HandshakeId { get; init; }
    public Guid InitiatorId { get; init; }
    public Guid RecipientId { get; init; }
    public required string InitiatorRole { get; init; }
    public Guid ItemId { get; init; }
    public DateTime Created { get; init; }
    public bool Deleted { get; set; }
}