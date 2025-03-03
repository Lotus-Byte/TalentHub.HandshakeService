using TalentHub.HandshakeService.Application.DTO;

namespace TalentHub.HandshakeService.Application.Interfaces;

public interface IUserService
{
    Task<PersonDto?> GetPersonAsync(Guid userId);
}