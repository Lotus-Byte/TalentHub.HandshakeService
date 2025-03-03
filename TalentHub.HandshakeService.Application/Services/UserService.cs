using System.Net.Http.Json;
using TalentHub.HandshakeService.Application.DTO;
using TalentHub.HandshakeService.Application.Interfaces;

namespace TalentHub.HandshakeService.Application.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PersonDto?> GetPersonAsync(Guid userId)
        {
            var ub = new UriBuilder("https", "localhost", 5000, $"api/Person/GetPersonAsync", $"?id={userId}");
            
            return await _httpClient.GetFromJsonAsync<PersonDto>(ub.Uri);
        }
    }
}
