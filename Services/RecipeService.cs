using CookbookClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CookbookClient.Services
{
    public class RecipeService
    {
        private readonly ApiClient _api;

        public RecipeService(ApiClient api)
        {
            _api = api;
        }

        public async Task<List<RecipeDto>> GetAllAsync()
            => await _api.Client.GetFromJsonAsync<List<RecipeDto>>("api/recipe");

        public async Task<RecipeDto> GetByIdAsync(int id)
            => await _api.Client.GetFromJsonAsync<RecipeDto>($"api/recipe/{id}");

        public async Task<RecipeDto> CreateAsync(RecipeCreateDto dto)
        {
            var response = await _api.Client.PostAsJsonAsync("api/recipe", dto);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<RecipeDto>();
        }

        public async Task<RecipeDto> UpdateAsync(RecipeUpdateDto dto)
        {
            var response = await _api.Client.PutAsJsonAsync($"api/recipe/{dto.Id}", dto);

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync());

            return await response.Content.ReadFromJsonAsync<RecipeDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _api.Client.DeleteAsync($"api/recipe/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Ошибка удаления");
        }
    }
}
