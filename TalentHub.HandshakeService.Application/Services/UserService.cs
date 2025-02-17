using System.Text.Json;
using TalentHub.HandshakeService.Application.DTO.User;
using TalentHub.HandshakeService.Application.Interfaces;

namespace TalentHub.HandshakeService.Application.Services
{
    public class UserService : IUserService
    {
        public async Task<UserDto?> GetUserAsync(Guid userId)
        {
            var httpClient = new HttpClient ();
            Stream stream;
            
            using (var resp = await httpClient.GetAsync("http://localhost:5000/GetPersonAsync?id=" + userId))
            {
                HttpResponseMessage resp2;
                
                if (!resp.IsSuccessStatusCode)
                    using (resp2 = await httpClient.GetAsync("http://localhost:5000/GetEmployerAsync?id=" + userId))
                    {
                        stream = await resp2.Content.ReadAsStreamAsync();
                    }
                else
                    stream = await resp.Content.ReadAsStreamAsync();
            }

            return await JsonSerializer.DeserializeAsync<UserDto>(stream);
        }
    }
}
