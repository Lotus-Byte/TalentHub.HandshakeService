namespace TalentHub.HandshakeService.Infrastructure.Models;

public class Handshake
{
    public Guid HandshakeId { get; init; }
    public Guid SenderUserId { get; init; }
    public Guid ReceiverUserId { get; init; }
    public required string SenderRole { get; init; }
    public Guid ItemId { get; init; }
    public DateTime Created { get; init; }
    public bool Deleted { get; set; }
}