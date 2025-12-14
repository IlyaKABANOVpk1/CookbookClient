using CookbookClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CookbookClient.Services
{
    public class AuthService
    {
        private readonly ApiClient _api;

        public AuthService(ApiClient api)
        {
            _api = api;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var response = await _api.Client.PostAsJsonAsync("api/auth/login", dto);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
            _api.SetToken(result.Token);

            // сохраним токен в SecureStorage
            await SecureStorage.SetAsync("token", result.Token);

            return result;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            var response = await _api.Client.PostAsJsonAsync("api/auth/register", dto);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
            _api.SetToken(result.Token);

            await SecureStorage.SetAsync("token", result.Token);

            return result;
        }
    }
}
