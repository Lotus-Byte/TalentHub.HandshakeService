namespace TalentHub.HandshakeService.Api.Configurations;

public class RabbitMqConfiguration
{
    public required string Host { get; init; }
    public required string VirtualHost { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string QueueName { get; init; }
}
