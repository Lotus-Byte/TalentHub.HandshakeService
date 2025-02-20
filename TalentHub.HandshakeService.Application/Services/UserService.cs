using System.Net.Http.Json;
using System.Text.Json;
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

        public async Task<UserDto?> GetUserAsync(SendHandshakeDto sendHandshakeDto)
        {
            var strRole = sendHandshakeDto.ToUserRole;
            var userId = sendHandshakeDto.ToUserId;

            return await _httpClient.GetFromJsonAsync<UserDto>($"http://localhost:5000/api/{strRole}Controller/Get{strRole}Async?id={userId}");
        }
    }
}
