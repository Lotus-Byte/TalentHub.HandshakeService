using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using TalentHub.HandshakeService.Application.DTO;
using TalentHub.HandshakeService.Application.Interfaces;

namespace TalentHub.HandshakeService.Application.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserService> _logger;

        public UserService (HttpClient httpClient, ILogger<UserService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        
        public async Task<UserContactsInfo> GetUserContactsAsync(Guid userId)
        {
            _logger.LogInformation($"Fetching contact information for user ID:'{userId}'");

            try
            {
                var response = await _httpClient.GetAsync($"Person/{userId}");
                
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                    {
                        _logger.LogWarning($"Contact information not found for user ID: {userId}");
                        throw new KeyNotFoundException($"User settings for user '{userId}' not found.");
                    }
                    case HttpStatusCode.BadRequest:
                    {
                        var errorDetails = await response.Content.ReadAsStringAsync();
                        _logger.LogWarning($"Bad request for user ID {userId}: {errorDetails}");
                        throw new ArgumentException($"Invalid request: {errorDetails}");
                    }
                    default:
                    {
                        response.EnsureSuccessStatusCode();
                        return await response.Content.ReadFromJsonAsync<UserContactsInfo>();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching contact information for user ID: {userId}");
                throw;
            } 
        }
    }
}
